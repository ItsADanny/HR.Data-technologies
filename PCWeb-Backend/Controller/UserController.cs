using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // ====================================================================================
        // GET
        // ====================================================================================
        [HttpGet("/userid/{id:int}")]
        public ActionResult<Account> GetByUserID(int id)
        {
            Account? user = Account.GetByID(id);
            if (user == null) return NotFound(new { message = "User not found." });
            return Ok(user);
        }

        [HttpGet("/roleid/{id:int}")]
        public ActionResult<Account> GetByRoleID(int id)
        {
            Account? user = Account.GetByRoleID(id);
            if (user == null) return NotFound(new { message = "User not found." });
            return Ok(user);
        }

        [HttpGet("/email/{email:alpha}")]
        public ActionResult<Account> GetByEmail(string email)
        {
            Account? user = Account.GetByEmail(email);
            if (user == null) return NotFound(new { message = "User not found." });
            return Ok(user);
        }

        [HttpGet("/phone/{phone:alpha}")]
        public ActionResult<Account> GetByPhone(string phone)
        {
            Account? user = Account.GetByPhone(phone);
            if (user == null) return NotFound(new { message = "User not found." });
            return Ok(user);
        }

        [HttpGet("/all")]
        public ActionResult<List<Account>> GetAllUsers()
        {
            return Ok(Account.GetAll());
        }

        // ====================================================================================
        // POST
        // ====================================================================================
        [HttpPost("")]
        public ActionResult<Account> CreateUser(UserDTO user)
        {
            // Create a new Account object
            Account newUser = new Account(user.FirstName, user.LastName, user.Email, "", user.Phone, user.Country);

            // Save to database
            var result = DBHandler.Create(newUser);

            //Return good or bad response 
            if (result == null) return BadRequest(new { message = "User registration failed." }); 
            
            //Set the password now that the user is created and we have the ID for the salt
            string[] hashedPassword = Auth.Hash(user.Password);

            newUser = (Account) result;
            newUser.Password = hashedPassword[0];
            DBHandler.Update(newUser);
            DBHandler.Create(new UserSalt(result.ID, hashedPassword[1]));
            
            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("/login")]
        public ActionResult<Account> LoginUser(UserLoginDTO user)
        {
            //Check if user exists
            Account? existingUser = Account.GetByEmail(user.Email);
            if (existingUser == null) return BadRequest(new { message = "Invalid email or password." });

            //Get the salt for the user
            UserSalt? salt = UserSalt.GetSalt(existingUser.ID);
            if (salt == null) return BadRequest(new { message = "Invalid email or password." });

            //Hash the provided password with the salt and compare to the stored hash
            string[] hashedPassword = Auth.Hash(user.Password, salt.Salt);

            if (hashedPassword[0] != existingUser.Password) return BadRequest(new { message = "Invalid email or password." });

            //If valid, create a session and return success
            UserSession newSession = new UserSession(existingUser.ID);
            DBHandler.Create(newSession);
            return Ok(new { message = "Login successful.", sessionToken = newSession.SessionToken });
        }

        // ====================================================================================
        // PUT
        // ====================================================================================
        [HttpPut("/userid/{id:int}")]
        public ActionResult<Account> UpdateUserByUserID(int id, Account user)
        {
            return Ok();
        }

        [HttpPut("/logout")]
        public ActionResult LogoutUser(string sessionToken)
        {
            //Check if session exists
            UserSession? existingSession = DBHandler.GetSessionByToken(sessionToken);
            if (existingSession == null) return BadRequest(new { message = "Invalid session token." });

            //Delete the session to log the user out
            bool result = DBHandler.Delete(existingSession);
            if (!result) return BadRequest(new { message = "Logout failed." });
            return Ok(new { message = "Logout successful." });
        }

        // ====================================================================================
        // DELETE
        // ====================================================================================
        [HttpDelete("/userid/{id:int}")]
        public ActionResult DeleteUserByUserID(int id)
        {
            return Ok();
        }
    }
}
