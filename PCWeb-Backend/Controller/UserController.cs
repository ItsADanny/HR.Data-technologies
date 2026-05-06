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
        public ActionResult<User> GetByUserID(int id)
        {
            return Ok();
        }

        [HttpGet("/roleid/{id:int}")]
        public ActionResult<User> GetByRoleID(int id)
        {
            return Ok();
        }

        [HttpGet("/email/{email:alpha}")]
        public ActionResult<User> GetByEmail(string email)
        {
            return Ok();
        }

        [HttpGet("/phone/{phone:alpha}")]
        public ActionResult<User> GetByPhone(string phone)
        {
            return Ok();
        }

        // ====================================================================================
        // POST
        // ====================================================================================
        [HttpPost("")]
        public ActionResult<User> CreateUser(UserDTO user)
        {
            // Create a new User object
            User newUser = new User(user.FirstName, user.LastName, user.Email, user.Password, user.Phone, user.Country);

            // Save to database
            var result = DBHandler.Create(newUser);

            //Return good or bad response 
            if (result == null) return BadRequest(new { message = "User registration failed." }); 
            return Ok(new { message = "User registered successfully." });
        }

        // ====================================================================================
        // PUT
        // ====================================================================================
        [HttpPut("/userid/{id:int}")]
        public ActionResult<User> UpdateUserByUserID(int id, User user)
        {
            return Ok();
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
