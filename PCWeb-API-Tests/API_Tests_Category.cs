namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;

[TestClass]
public sealed class API_Tests_Category : BaseTest
{
    // ====================================================================================
    // TESTS
    // ====================================================================================

    [TestMethod]
    public void GetCategoryTest()
    {
        CategoryController categoryController = new CategoryController();

        var result = categoryController.GetAllCategories();
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }
}