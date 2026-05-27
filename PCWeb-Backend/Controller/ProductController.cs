using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // ====================================================================================
        // GET
        // ====================================================================================

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

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product ID");

            var product = Product.ReadProductByID(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpGet("")]
        public IActionResult GetProducts(int page = 1, int pageSize = 100)
        {
            if (page < 1)
                return BadRequest("Page must be greater than 0");

            if (pageSize <= 0)
                return BadRequest("PageSize must be a positive integer");

            int offset = (page - 1) * pageSize;
            var products = Product.ReadProducts(pageSize, offset);

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }

        [HttpGet("all")]
        public IActionResult GetProducts()
        {
            var products = Product.ReadProducts();

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }

        // ====================================================================================
        // POST
        // ====================================================================================
        // [HttpPost("{categoryId:int}")]
        // public IActionResult CreateProduct(int categoryId, [FromBody] Product product)
        // {
        //     if (product == null)
        //         return BadRequest("Invalid product data");

        //     Product? createdProduct = Product.CreateProduct(categoryId, product);

        //     if (createdProduct == null)
        //         return StatusCode(500, "Error creating product");

        //     return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        // }

        // ====================================================================================
        // PUT
        // ====================================================================================
        // [HttpPut("{id:int}")]
        // public IActionResult UpdateProduct(int id, [FromBody] Product product)
        // {
        //     if (product == null) return BadRequest("Invalid product data");

        //     Product? existingProduct = Product.ReadProductByID(id);
        //     if (existingProduct == null) return NotFound("Product not found");

        //     bool updatedProduct = Product.UpdateProduct(id, product);
        //     if (!updatedProduct) return StatusCode(500, "Error updating product");

        //     return Ok(updatedProduct);
        // }  

        // ====================================================================================
        // DELETE
        // ====================================================================================
        // [HttpDelete("{id:int}")]
        // public IActionResult DeleteProduct(int id)
        // {
        //     Product? existingProduct = Product.ReadProductByID(id);
        //     if (existingProduct == null) return NotFound("Product not found");

        //     bool result = Product.DeleteProduct(id);
        //     if (!result) return StatusCode(500, "Error deleting product");
        //     return Ok();
        // }
    }
}
