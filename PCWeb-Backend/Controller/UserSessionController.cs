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
        [HttpGet("session/userid/{id:int}")]
        public ActionResult<Account> GetByUserID(int id)
        {
            return Ok();
        }

        [HttpGet("session/sessiontoken/{token}")]
        public ActionResult<UserSession> GetByUserSession(string token)
        {
            UserSession? session = DBHandler.GetSessionByToken(token);
            if (session == null) return NotFound(new { message = "Session not found or expired." });

            return Ok(session);
        }

        // ====================================================================================
        // POST
        // ====================================================================================
        [HttpPost("")]
        public ActionResult<Account> CreateUserSession(UserSession session)
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
