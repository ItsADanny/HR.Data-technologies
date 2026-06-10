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

        [HttpGet("all")]
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

                string? validationError = ValidateAddress(address);
                if (validationError != null)
                    return BadRequest(new { message = validationError });

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
        // PUT
        // ====================================================================================

        [HttpPut("{id:int}")]
        public IActionResult UpdateAddress(int id, [FromBody] Address address)
        {
            try
            {
                if (address == null)
                    return BadRequest(new { message = "Address data is required" });

                string? validationError = ValidateAddress(address);
                if (validationError != null)
                    return BadRequest(new { message = validationError });

                var existingAddress = Address.GetById(id);
                if (existingAddress == null)
                    return NotFound(new { message = "Address not found" });

                address.AddressId = id;
                var result = DBHandler.Update(address);

                if (!result)
                    return StatusCode(500, new { message = "Error updating address in database" });

                return Ok(new { message = "Address updated successfully" });
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

        // ====================================================================================
        // VALIDATION
        // ====================================================================================

        private static string? ValidateAddress(Address address)
        {
            if (string.IsNullOrWhiteSpace(address.Street))
                return "Street is required";

            if (string.IsNullOrWhiteSpace(address.City))
                return "City is required";

            if (string.IsNullOrWhiteSpace(address.PostCode))
                return "Postcode is required";

            if (string.IsNullOrWhiteSpace(address.Country))
                return "Country is required";

            if (address.HouseNumber <= 0)
                return "House number must be greater than 0";

            if (address.UserId <= 0)
                return "A valid user ID is required";

            return null;
        }
    }
}
