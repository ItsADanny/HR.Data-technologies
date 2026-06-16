namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;

[TestClass]
public sealed class API_Tests_Address : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    // Existing test account ID (should be set to a valid user in the database for the tests to work)
    public static int test_user_ID_alreadyInDB = TestConfiguration.USER_ID;

    /// Address created during tests, reused for update and delete
    public static Address? test_createdAddress;

    // ====================================================================================
    // HELPERS
    // ====================================================================================

    private Address CreateValidTestAddress()
    {
        return new Address
        {
            Country = GeneralTestingMethods.random_country(),
            City = GeneralTestingMethods.random_name(),
            Street = GeneralTestingMethods.random_string(8),
            HouseNumber = new Random().Next(1, 999),
            HouseNumberAddition = "A",
            PostCode = GeneralTestingMethods.random_string(6),
            UserId = test_user_ID_alreadyInDB
        };
    }

    // ====================================================================================
    // GET TESTS
    // ====================================================================================

    // 1. Test that all addresses can be retrieved successfully
    [TestMethod]
    public void GetAllAddressesTest()
    {
        AddressController addressController = new AddressController();
        var result = addressController.GetAddresses();
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 2. Test that an address can be retrieved successfully by ID
    // [TestMethod]
    // public void GetAddressByIdTest_Success()
    // {
    //     AddressController addressController = new AddressController();

    //     // First create an address
    //     Address newAddress = CreateValidTestAddress();
    //     var createResult = addressController.CreateAddress(newAddress);
    //     Assert.IsInstanceOfType(createResult, typeof(OkObjectResult));

    //     // Extract the created address ID from the result
    //     var okResult = (createResult as OkObjectResult)!;
    //     var anonResult = okResult.Value!;
    //     var type = anonResult.GetType();
    //     var addressProp = type.GetProperty("address")!.GetValue(anonResult) as Address;
    //     Assert.IsNotNull(addressProp);
    //     test_createdAddress = addressProp;

    //     var getResult = addressController.GetAddressById(test_createdAddress.AddressId);
    //     Assert.IsInstanceOfType(getResult, typeof(OkObjectResult));
    // }

    // 3. Test that retrieving a non-existent address returns NotFound
    [TestMethod]
    public void GetAddressByIdTest_NotFound()
    {
        AddressController addressController = new AddressController();
        var result = addressController.GetAddressById(int.MaxValue);
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    // 4. Test that addresses can be retrieved successfully by user ID
    [TestMethod]
    public void GetAddressesByUserIdTest()
    {
        AddressController addressController = new AddressController();
        var result = addressController.GetAddressesByUserId(test_user_ID_alreadyInDB);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // ====================================================================================
    // POST (CREATE) TESTS
    // ====================================================================================

    // 5. Test that a valid address can be created successfully
    // [TestMethod]
    // public void CreateAddressTest_Success()
    // {
    //     AddressController addressController = new AddressController();
    //     Address newAddress = CreateValidTestAddress();
    //     var result = addressController.CreateAddress(newAddress);
    //     Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    // }

    // 6. Test that creating an address with null body returns BadRequest
    [TestMethod]
    public void CreateAddressTest_NullBody()
    {
        AddressController addressController = new AddressController();
        var result = addressController.CreateAddress(null!);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 7. Test that creating an address missing Street returns BadRequest
    [TestMethod]
    public void CreateAddressTest_MissingStreet()
    {
        AddressController addressController = new AddressController();
        Address invalidAddress = CreateValidTestAddress();
        invalidAddress.Street = "";
        var result = addressController.CreateAddress(invalidAddress);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 8. Test that creating an address missing City returns BadRequest
    [TestMethod]
    public void CreateAddressTest_MissingCity()
    {
        AddressController addressController = new AddressController();
        Address invalidAddress = CreateValidTestAddress();
        invalidAddress.City = "";
        var result = addressController.CreateAddress(invalidAddress);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 9. Test that creating an address missing PostCode returns BadRequest
    [TestMethod]
    public void CreateAddressTest_MissingPostCode()
    {
        AddressController addressController = new AddressController();
        Address invalidAddress = CreateValidTestAddress();
        invalidAddress.PostCode = "";
        var result = addressController.CreateAddress(invalidAddress);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 10. Test that creating an address missing Country returns BadRequest
    [TestMethod]
    public void CreateAddressTest_MissingCountry()
    {
        AddressController addressController = new AddressController();
        Address invalidAddress = CreateValidTestAddress();
        invalidAddress.Country = "";
        var result = addressController.CreateAddress(invalidAddress);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 11. Test that creating an address with invalid HouseNumber returns BadRequest
    [TestMethod]
    public void CreateAddressTest_InvalidHouseNumber()
    {
        AddressController addressController = new AddressController();
        Address invalidAddress = CreateValidTestAddress();
        invalidAddress.HouseNumber = 0;
        var result = addressController.CreateAddress(invalidAddress);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 12. Test that creating an address with invalid UserId returns BadRequest
    [TestMethod]
    public void CreateAddressTest_InvalidUserId()
    {
        AddressController addressController = new AddressController();
        Address invalidAddress = CreateValidTestAddress();
        invalidAddress.UserId = 0;
        var result = addressController.CreateAddress(invalidAddress);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // ====================================================================================
    // PUT (UPDATE) TESTS
    // ====================================================================================

    // 13. Test that an existing address can be updated successfully
    // [TestMethod]
    // public void UpdateAddressTest_Success()
    // {
    //     AddressController addressController = new AddressController();

    //     // Create an address first
    //     Address newAddress = CreateValidTestAddress();
    //     var createResult = addressController.CreateAddress(newAddress);
    //     Assert.IsInstanceOfType(createResult, typeof(OkObjectResult));

    //     var okResult = (createResult as OkObjectResult)!;
    //     var anonResult = okResult.Value!;
    //     var type = anonResult.GetType();
    //     var addressProp = type.GetProperty("address")!.GetValue(anonResult) as Address;
    //     Assert.IsNotNull(addressProp);
    //     test_createdAddress = addressProp;

    //     // Update the address
    //     Address updatedAddress = new Address
    //     {
    //         Country = "UpdatedCountry",
    //         City = "UpdatedCity",
    //         Street = "UpdatedStreet",
    //         HouseNumber = 999,
    //         HouseNumberAddition = "B",
    //         PostCode = "99999",
    //         UserId = test_user_ID_alreadyInDB
    //     };

    //     var updateResult = addressController.UpdateAddress(test_createdAddress.AddressId, updatedAddress);
    //     Assert.IsInstanceOfType(updateResult, typeof(OkObjectResult));
    // }

    // 14. Test that updating a non-existent address returns NotFound
    [TestMethod]
    public void UpdateAddressTest_NotFound()
    {
        AddressController addressController = new AddressController();
        Address updatedAddress = CreateValidTestAddress();
        var result = addressController.UpdateAddress(int.MaxValue, updatedAddress);
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    // 15. Test that updating with null body returns BadRequest
    [TestMethod]
    public void UpdateAddressTest_NullBody()
    {
        AddressController addressController = new AddressController();
        var result = addressController.UpdateAddress(1, null!);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 16. Test that updating with invalid data returns BadRequest
    [TestMethod]
    public void UpdateAddressTest_InvalidData()
    {
        AddressController addressController = new AddressController();
        Address invalidAddress = CreateValidTestAddress();
        invalidAddress.Street = ""; // missing street
        var result = addressController.UpdateAddress(1, invalidAddress);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // ====================================================================================
    // DELETE TESTS
    // ====================================================================================

    // 17. Test that an existing address can be deleted successfully
    // [TestMethod]
    // public void DeleteAddressTest_Success()
    // {
    //     AddressController addressController = new AddressController();

    //     // Create an address first
    //     Address newAddress = CreateValidTestAddress();
    //     var createResult = addressController.CreateAddress(newAddress);
    //     Assert.IsInstanceOfType(createResult, typeof(OkObjectResult));

    //     var okResult = (createResult as OkObjectResult)!;
    //     var anonResult = okResult.Value!;
    //     var type = anonResult.GetType();
    //     var addressProp = type.GetProperty("address")!.GetValue(anonResult) as Address;
    //     Assert.IsNotNull(addressProp);
    //     test_createdAddress = addressProp;

    //     // Delete the address
    //     var deleteResult = addressController.DeleteAddress(test_createdAddress.AddressId);
    //     Assert.IsInstanceOfType(deleteResult, typeof(OkObjectResult));
    // }
}