using Microsoft.AspNetCore.Mvc;

[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public ActionResult<string> Register([FromBody] registerDto request)
    {
        // Create a new User object
        User newUser = new User(0, request.firstName, request.lastName, request.email, request.password);

        // Set creation date and time
        newUser.CreateDateTime = DateTime.Now;

        // Save to database
        var result = DBHandler.Create(newUser);

        //Return good or bad response 
        if (result == null) return BadRequest("User registration failed.");  
        return Ok("User registered successfully.");
    }
}