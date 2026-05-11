using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("with-category")]
        public IActionResult GetProductsWithCategory(int categoryId)
        {
            var products = DBHandler.ReadAllProductsWithCategory(categoryId);
            return Ok(products);
        }
    }
}
