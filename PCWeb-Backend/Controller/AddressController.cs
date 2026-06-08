using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // ====================================================================================
        // GET
        // ====================================================================================

        [HttpGet("get-all")]
        public IActionResult GetAddresses()
        {
            try
            {
                var addresses = Address.GetAll();
                
                if (addresses == null)
                    return StatusCode(500, new { message = "Error retrieving addresses from database" });

                return Ok(addresses);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal server error", error = e.Message });
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAddressById(int id)
        {
            try
            {
                var address = Address.GetById(id);
                
                if (address == null)
                    return NotFound(new { message = "Address not found" });

                return Ok(address);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal server error", error = e.Message });
            }
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetAddressesByUserId(int userId)
        {
            try
            {
                var addresses = Address.GetByUserId(userId);
                
                if (addresses == null)
                    return StatusCode(500, new { message = "Error retrieving addresses from database" });

                return Ok(addresses);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal server error", error = e.Message });
            }
        }
        
        // ====================================================================================
        // POST
        // ====================================================================================

        [HttpPost("create")]
        public IActionResult CreateAddress([FromBody] Address address)
        {
            try
            {
                if (address == null)
                    return BadRequest(new { message = "Address data is required" });

                var result = DBHandler.Create(address);
                
                if (result == null)
                    return StatusCode(500, new { message = "Error creating address in database" });

                return Ok(new { message = "Address created successfully", address = result });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal server error", error = e.Message });
            }
        }

        // ====================================================================================
        // DELETE
        // ====================================================================================

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAddress(int id)
        {
            try
            {
                var address = new Address { AddressId = id };
                var result = DBHandler.Delete(address);
                
                if (!result)
                    return StatusCode(500, new { message = "Error deleting address from database" });

                return Ok(new { message = "Address deleted successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal server error", error = e.Message });
            }
        }
    }
}
