using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("with-category")]
        public IActionResult GetProductsWithCategory(int categoryId, int page = 1, int pageSize = 100)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid categoryId");

            if (page < 1)
                return BadRequest("Page must be greater than 0");

            int offset = (page - 1) * pageSize;
            var products = Product.ReadAllProductsWithCategory(categoryId, pageSize, offset);
            
            if (products == null)
                return StatusCode(500, "Error retrieving products from database");

            return Ok(products);
        }
    }
}
