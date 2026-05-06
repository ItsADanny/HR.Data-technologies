using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSessionController : ControllerBase
    {
        // ====================================================================================
        // GET
        // ====================================================================================
        [HttpGet("/userid/{id:int}")]
        public ActionResult<User> GetByUserID(int id)
        {
            return Ok();
        }

        [HttpGet("/sessiontoken/{token:alpha}")]
        public ActionResult<User> GetByUserSession(string token)
        {
            return Ok();
        }

        // ====================================================================================
        // POST
        // ====================================================================================
        [HttpPost("")]
        public ActionResult<User> CreateUserSession(UserSession session)
        {
            return Ok();
        }

        // ====================================================================================
        // DELETE
        // ====================================================================================
        [HttpDelete("/sessiontoken/{token:alpha}")]
        public ActionResult DeleteUserSession(string token)
        {
            return Ok();
        }
    }
}
