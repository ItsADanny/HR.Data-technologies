namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;
using PCWeb_Backend.DTO;

[TestClass]
public sealed class API_Tests_Order : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    // Existing test user ID (should be set to a valid user in the database)
    public static int test_user_ID_alreadyInDB = TestConfiguration.USER_ID;

    // Existing test address ID (should be set to a valid address in the database)
    public static int test_address_ID_alreadyInDB = TestConfiguration.ADDRESS_ID;

    // Existing test product ID (should be set to a valid product in the database)
    public static int test_product_ID_alreadyInDB = TestConfiguration.PRODUCT_ID;

    // ====================================================================================
    // HELPERS
    // ====================================================================================

    private CreateOrderDTO CreateValidOrderDTO()
    {
        return new CreateOrderDTO
        {
            userId = test_user_ID_alreadyInDB,
            shippingAddressId = test_address_ID_alreadyInDB,
            billingAddressId = test_address_ID_alreadyInDB,
            cartItems = new List<CartItemDTO>
            {
                new CartItemDTO
                {
                    id = test_product_ID_alreadyInDB,
                    name = "Test Product",
                    price = 99.99,
                    quantity = 1
                }
            }
        };
    }

    // ====================================================================================
    // TESTS
    // ====================================================================================

    // -- POST (CREATE) TESTS --

    // 1. Test that a valid order can be created successfully
    // [TestMethod]
    // public void CreateOrderTest_Success()
    // {
    //     OrderController controller = new OrderController();
    //     CreateOrderDTO dto = CreateValidOrderDTO();
    //     var result = controller.CreateOrder(dto);
    //     Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    // }

    // 2. Test that creating an order with null body returns BadRequest
    // [TestMethod]
    // public void CreateOrderTest_NullBody()
    // {
    //     OrderController controller = new OrderController();
    //     var result = controller.CreateOrder(null!);
    //     Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    // }

    // 3. Test that creating an order with invalid user ID returns BadRequest
    [TestMethod]
    public void CreateOrderTest_InvalidUserId()
    {
        OrderController controller = new OrderController();
        CreateOrderDTO dto = CreateValidOrderDTO();
        dto.userId = 0;
        var result = controller.CreateOrder(dto);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 4. Test that creating an order with invalid shipping address returns BadRequest
    [TestMethod]
    public void CreateOrderTest_InvalidShippingAddress()
    {
        OrderController controller = new OrderController();
        CreateOrderDTO dto = CreateValidOrderDTO();
        dto.shippingAddressId = 0;
        var result = controller.CreateOrder(dto);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 5. Test that creating an order with invalid billing address returns BadRequest
    [TestMethod]
    public void CreateOrderTest_InvalidBillingAddress()
    {
        OrderController controller = new OrderController();
        CreateOrderDTO dto = CreateValidOrderDTO();
        dto.billingAddressId = 0;
        var result = controller.CreateOrder(dto);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}