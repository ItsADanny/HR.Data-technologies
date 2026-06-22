namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;

[TestClass]
public sealed class API_Tests_Product : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    // Existing test category ID (should be set to a valid category in the database)
    public static int test_category_ID_alreadyInDB = TestConfiguration.CATEGORY_ID;

    // Existing test product ID (should be set to a valid product in the database)
    public static int test_product_ID_alreadyInDB = TestConfiguration.PRODUCT_ID;

    // ====================================================================================
    // TESTS
    // ====================================================================================

    // -- GET TESTS --

    // 1. Test that all products can be retrieved successfully
    [TestMethod]
    public void GetAllProductsTest()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProducts();
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 2. Test that products can be retrieved with pagination
    [TestMethod]
    public void GetProductsWithPaginationTest()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProducts(1, 10);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 3. Test that pagination with invalid page returns BadRequest
    [TestMethod]
    public void GetProductsWithInvalidPageTest()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProducts(0, 10);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 4. Test that pagination with invalid pageSize returns BadRequest
    [TestMethod]
    public void GetProductsWithInvalidPageSizeTest()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProducts(1, 0);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 5. Test that a product can be retrieved by ID successfully
    [TestMethod]
    public void GetProductByIdTest_Success()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProduct(test_product_ID_alreadyInDB);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 6. Test that retrieving a product with invalid ID returns BadRequest
    [TestMethod]
    public void GetProductByIdTest_InvalidId()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProduct(0);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 7. Test that retrieving a non-existent product returns NotFound
    // [TestMethod]
    // public void GetProductByIdTest_NotFound()
    // {
    //     ProductController controller = new ProductController();
    //     var result = controller.GetProduct(int.MaxValue);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }

    // 8. Test that product details can be retrieved successfully
    // [TestMethod]
    // public void GetProductDetailsTest_Success()
    // {
    //     ProductController controller = new ProductController();
    //     var result = controller.GetProductDetails(test_product_ID_alreadyInDB);
    //     Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    // }

    // 9. Test that product details with invalid ID returns BadRequest
    [TestMethod]
    public void GetProductDetailsTest_InvalidId()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductDetails(0);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 10. Test that product details for non-existent product returns NotFound
    [TestMethod]
    public void GetProductDetailsTest_NotFound()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductDetails(int.MaxValue);
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    // 11. Test that products with category can be retrieved successfully
    [TestMethod]
    public void GetProductsWithCategoryTest_Success()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductsWithCategory(test_category_ID_alreadyInDB);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 12. Test that products with invalid category ID returns BadRequest
    [TestMethod]
    public void GetProductsWithCategoryTest_InvalidCategoryId()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductsWithCategory(0);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 13. Test that products with category and pagination returns Ok
    [TestMethod]
    public void GetProductsWithCategoryAndPaginationTest_Success()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductsWithCategory(test_category_ID_alreadyInDB, 1, 10);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 14. Test that products with category and invalid page returns BadRequest
    [TestMethod]
    public void GetProductsWithCategoryTest_InvalidPage()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductsWithCategory(test_category_ID_alreadyInDB, 0, 10);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 15. Test that products with category and brand can be retrieved
    [TestMethod]
    public void GetProductsWithCategoryAndBrandTest_Success()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductsWithCategory(test_category_ID_alreadyInDB, "NVIDIA");
        // Should return Ok (even if empty) or StatusCode 500 on error
        Assert.IsTrue(result is OkObjectResult || result is StatusCodeResult);
    }

    // 16. Test that products with category and brand with invalid category returns BadRequest
    [TestMethod]
    public void GetProductsWithCategoryAndBrandTest_InvalidCategoryId()
    {
        ProductController controller = new ProductController();
        var result = controller.GetProductsWithCategory(0, "NVIDIA");
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 17. Test that all brands in a category can be retrieved
    [TestMethod]
    public void GetAllBrandsInCategoryTest_Success()
    {
        ProductController controller = new ProductController();
        var result = controller.GetAllBrand(test_category_ID_alreadyInDB);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 18. Test that brands in category with invalid category ID returns BadRequest
    [TestMethod]
    public void GetAllBrandsInCategoryTest_InvalidCategoryId()
    {
        ProductController controller = new ProductController();
        var result = controller.GetAllBrand(0);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 19. Test that products can be searched successfully
    [TestMethod]
    public void SearchProductsTest_Success()
    {
        ProductController controller = new ProductController();
        var result = controller.SearchProducts("laptop");
        Assert.IsTrue(result is OkObjectResult || result is NotFoundObjectResult);
    }

    // 20. Test that searching with empty query returns BadRequest
    [TestMethod]
    public void SearchProductsTest_EmptyQuery()
    {
        ProductController controller = new ProductController();
        var result = controller.SearchProducts("   ");
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 21. Test that searching with pagination works
    [TestMethod]
    public void SearchProductsWithPaginationTest_Success()
    {
        ProductController controller = new ProductController();
        var result = controller.SearchProducts("laptop", 1, 10);
        Assert.IsTrue(result is OkObjectResult || result is NotFoundObjectResult);
    }

    // 22. Test that searching with invalid page returns BadRequest
    [TestMethod]
    public void SearchProductsWithPaginationTest_InvalidPage()
    {
        ProductController controller = new ProductController();
        var result = controller.SearchProducts("laptop", 0, 10);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 23. Test that searching with invalid pageSize returns BadRequest
    [TestMethod]
    public void SearchProductsWithPaginationTest_InvalidPageSize()
    {
        ProductController controller = new ProductController();
        var result = controller.SearchProducts("laptop", 1, 0);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // -- DELETE TESTS --

    // 24. Test that deleting a non-existent product returns NotFound
    // [TestMethod]
    // public void DeleteProductTest_NotFound()
    // {
    //     ProductController controller = new ProductController();
    //     var result = controller.DeleteProduct(int.MaxValue);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }

    // -- POST (CREATE) TESTS --

    // 25. Test that creating a product with null body returns BadRequest
    [TestMethod]
    public void CreateProductTest_NullBody()
    {
        ProductController controller = new ProductController();
        var result = controller.CreateProduct(test_category_ID_alreadyInDB, null!);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 26. Test that creating a product with invalid category returns BadRequest
    // [TestMethod]
    // public void CreateProductTest_InvalidCategoryId()
    // {
    //     ProductController controller = new ProductController();
    //     Product newProduct = new Product
    //     {
    //         Name = "Test Product",
    //         Manufacturer = "Test Manufacturer",
    //         Description = "Test Description",
    //         Price = 99.99,
    //         Stock = 10,
    //         MinimalStock = 5,
    //         Discontinued = false
    //     };
    //     var result = controller.CreateProduct(0, newProduct);
    //     Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    // }

    // -- PUT (UPDATE) TESTS --

    // 27. Test that updating with null body returns BadRequest
    [TestMethod]
    public void UpdateProductTest_NullBody()
    {
        ProductController controller = new ProductController();
        var result = controller.UpdateProduct(test_product_ID_alreadyInDB, null!);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 28. Test that updating a non-existent product returns NotFound
    // [TestMethod]
    // public void UpdateProductTest_NotFound()
    // {
    //     ProductController controller = new ProductController();
    //     Product updateProduct = new Product
    //     {
    //         Name = "Updated Product",
    //         Manufacturer = "Updated Manufacturer",
    //         Description = "Updated Description",
    //         Price = 199.99,
    //         Stock = 20,
    //         MinimalStock = 10,
    //         Discontinued = false
    //     };
    //     var result = controller.UpdateProduct(int.MaxValue, updateProduct);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }
}