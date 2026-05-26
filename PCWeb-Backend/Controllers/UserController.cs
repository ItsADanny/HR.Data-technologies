using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public ActionResult<string> Register([FromBody] registerDto request)
    {
        // Create a new Account object
        Account newUser = new Account(request.firstName, request.lastName, request.email, request.password, "", "");

        // Set creation date and time
        newUser.CreateDateTime = DateTime.Now;

        // Save to database
        var result = DBHandler.Create(newUser);

        //Return good or bad response 
        if (result == null) return BadRequest(new { message = "User registration failed." }); 
        return Ok(new { message = "User registered successfully." });
    }
}
