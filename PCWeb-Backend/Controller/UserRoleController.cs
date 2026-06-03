using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        [HttpGet]
        public ActionResult<UserRole> GetUserRole(int id)
        {
            var userRole = UserRole.GetUserRoleByID(id);
            if (userRole == null) {
                return NotFound();
            }
            return Ok(userRole);
        }

        [HttpGet("all")]
        public ActionResult<List<UserRole>> GetAllUserRoles()
        {
            var userRoles = UserRole.userRoles();
            if (userRoles == null) {
                return NotFound();
            }
            return Ok(userRoles);
        }

        [HttpPost]
        public ActionResult<UserRole> CreateUserRole(UserRole userRole)
        {
            if (!UserRole.CreateUserRole(userRole)) {
                return BadRequest();
            }
            return Ok(userRole);
        }

        [HttpPut]
        public ActionResult<UserRole> UpdateUserRole(UserRole userRole)
        {
            if (!UserRole.UpdateUserRole(userRole)) {
                return BadRequest();
            }
            return Ok(userRole);
        }

        [HttpDelete]
        public ActionResult<UserRole> DeleteUserRole(int id)
        {
            if (!UserRole.DeleteUserRole(id)) {
                return BadRequest();
            }
            return Ok();
        }
    }
}
