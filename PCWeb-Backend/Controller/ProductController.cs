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
            int offset = (page - 1) * pageSize;
            var products = Product.ReadAllProductsWithCategory(categoryId, pageSize, offset);
            return Ok(products);
        }
    }
}
