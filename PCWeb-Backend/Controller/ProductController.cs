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

        [HttpGet("with-category/{categoryId:int}/with-brand/{brand:alpha}")]
        public IActionResult GetProductsWithCategory(int categoryId, string brand, int page = 1, int pageSize = 100)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid categoryId");

            if (page < 1)
                return BadRequest("Page must be greater than 0");

            int offset = (page - 1) * pageSize;
            var products = Product.ReadAllProductsWithCategoryWithBrand(categoryId, brand, pageSize, offset);
            
            if (products == null)
                return StatusCode(500, "Error retrieving products from database");

            return Ok(products);
        }

        [HttpGet("GetAllBrandsThatInSameCategory/{categoryId:int}")]
        public IActionResult GetAllBrand(int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid category ID");

            var brand = Product.ReadAllBrandsInSameCategory(categoryId);

            if (brand == null)
                return NotFound("Brand not found for the given category ID");

            return Ok(brand);
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

        [HttpGet("search/{query:alpha}")]
        public IActionResult SearchProducts(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query cannot be empty");

            var products = Product.SearchProducts(query);

            if (products == null)
                return NotFound("Products not found");

            return Ok(products);
        }

        [HttpGet("search/{query:alpha}/{page:int}/{pageSize:int}")]
        public IActionResult SearchProducts(string query, int page = 1, int pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query cannot be empty");
            if (page < 1)
                return BadRequest("Page must be greater than 0");
            if (pageSize <= 0)
                return BadRequest("PageSize must be a positive integer");
            
            int offset = (page - 1) * pageSize;
            var products = Product.SearchProducts(query, pageSize, offset);

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

        //     List<ProductWithFieldsDTO>? createdProduct = Product.CreateProduct(categoryId, product);

        //     if (createdProduct == null)
        //         return StatusCode(500, "Error creating product");

        //     return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.First().Id }, createdProduct);
        // }

        // ====================================================================================
        // PUT
        // ====================================================================================
        // [HttpPut("{id:int}")]
        // public IActionResult UpdateProduct(int id, [FromBody] Product product)
        // {
        //     if (product == null) return BadRequest("Invalid product data");

        //     List<ProductWithFieldsDTO>? existingProduct = Product.ReadProductByID(id);
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
        //     List<ProductWithFieldsDTO>? existingProduct = Product.ReadProductByID(id);
        //     if (existingProduct == null) return NotFound("Product not found");

        //     bool result = Product.DeleteProduct(id);
        //     if (!result) return StatusCode(500, "Error deleting product");
        //     return Ok();
        // }
    }
}
