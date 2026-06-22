using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // ====================================================================================
        // GET
        // ====================================================================================

        [HttpGet("all")]
        public IActionResult GetAllCategories()
        {
            var categories = Categories.ReadAllCategories();

            if (categories == null)
                return StatusCode(500, "Error retrieving categories from database");

            return Ok(categories);
        }
    }
}