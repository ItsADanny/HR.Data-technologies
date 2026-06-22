namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;

[TestClass]
public sealed class API_Tests_Users : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    public static int test_account_ID_alreadyInDB = TestConfiguration.USER_ID;
    public static int test_account_RoleID_alreadyInDB = TestConfiguration.USER_ROLE_ID;
    public static string test_account_Email_alreadyInDB = TestConfiguration.USER_EMAIL;
    public static string test_account_Phone_alreadyInDB = "";

    // 1. Test that a user can be retrieved successfully by ID
    // [TestMethod]
    // public void GetUserByIDTest()
    // {
    //     UserController userController = new UserController();
    //     var result = userController.GetByUserID(test_account_ID_alreadyInDB);
    //     Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    // }

    //2. Test that a user can be retrieved successfully by Role ID
    [TestMethod]
    public void GetUserByRoleIDTest()
    {
        UserController userController = new UserController();
        var result = userController.GetByRoleID(test_account_RoleID_alreadyInDB);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    //3. Test that a user can be retrieved successfully by Email
    // [TestMethod]
    // public void GetUserByEmailTest()
    // {
    //     UserController userController = new UserController();
    //     var result = userController.GetByEmail(test_account_Email_alreadyInDB);
    //     Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    // }

    //4. Test that a user can be retrieved successfully by Phone
    [TestMethod]
    public void GetUserByPhoneTest()
    {
        UserController userController = new UserController();
        var result = userController.GetByPhone(test_account_Phone_alreadyInDB);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    //5. Test that all users can be retrieved successfully
    [TestMethod]
    public void GetAllUsersTest()
    {
        UserController userController = new UserController();
        var result = userController.GetAllUsers();
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    //6. Test that a user can be created successfully
    [TestMethod]
    public void CreateUserTest()
    {
        UserController userController = new UserController();
        UserDTO newUser = new UserDTO(GeneralTestingMethods.random_name(), GeneralTestingMethods.random_name(), GeneralTestingMethods.random_email(), GeneralTestingMethods.random_string(10), GeneralTestingMethods.random_phone(), GeneralTestingMethods.random_country());
        var result = userController.CreateUser(newUser);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    // 7. Test that a user can be deleted successfully
    // [TestMethod]
    // public void DeleteUserTest()
    // {
    //     UserController userController = new UserController();
    //     UserDTO newUser = new UserDTO(GeneralTestingMethods.random_name(), GeneralTestingMethods.random_name(), GeneralTestingMethods.random_email(), GeneralTestingMethods.random_string(10), GeneralTestingMethods.random_phone(), GeneralTestingMethods.random_country());
    //     var createResult = userController.CreateUser(newUser);
    //     Assert.IsInstanceOfType(createResult.Result, typeof(OkObjectResult));
    //     Account? createdUser = (createResult.Result as OkObjectResult)?.Value as Account;
    //     Assert.IsNotNull(createdUser);
    //     var deleteResult = userController.DeleteUserByUserID(createdUser.ID);
    //     Assert.IsInstanceOfType(deleteResult, typeof(OkObjectResult));
    // }

    // -- PUT (UPDATE) TESTS --

    // 8. Test that updating a user with a non-existent ID returns NotFound
    [TestMethod]
    public void UpdateUserTest_NotFound()
    {
        UserController userController = new UserController();
        UpdateAccountDTO dto = new UpdateAccountDTO
        {
            First_Name = "UpdatedFirstName",
            Last_Name = "UpdatedLastName",
            Email = "updated@example.com",
            Phone = "1234567890",
            Country = GeneralTestingMethods.random_country()
        };
        var result = userController.UpdateUserByUserID(int.MaxValue, dto);
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    // 9. Test that resetting password with a non-existent user ID returns NotFound
    [TestMethod]
    public void ResetPasswordTest_NotFound()
    {
        UserController userController = new UserController();
        ResetPasswordDTO request = new ResetPasswordDTO(GeneralTestingMethods.random_string(12));
        var result = userController.ResetPassword(int.MaxValue, request);
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    // 10. Test that logging out with invalid session token returns BadRequest
    [TestMethod]
    public void LogoutUserTest_InvalidSessionToken()
    {
        UserController userController = new UserController();
        var result = userController.LogoutUser("invalid-session-token-" + Guid.NewGuid().ToString("N"));
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 11. Test that logging out with null session token returns BadRequest
    [TestMethod]
    public void LogoutUserTest_NullSessionToken()
    {
        UserController userController = new UserController();
        var result = userController.LogoutUser(null!);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // -- NEGATIVE (NOT FOUND) TESTS --

    // 12. Test that retrieving a user with invalid ID returns NotFound
    [TestMethod]
    public void GetUserByIDTest_NotFound()
    {
        UserController userController = new UserController();
        var result = userController.GetByUserID(int.MaxValue);
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    // 13. Test that retrieving a user with invalid role ID returns NotFound
    [TestMethod]
    public void GetUserByRoleIDTest_NotFound()
    {
        UserController userController = new UserController();
        var result = userController.GetByRoleID(int.MaxValue);
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    // 14. Test that retrieving a user with invalid email returns NotFound
    [TestMethod]
    public void GetUserByEmailTest_NotFound()
    {
        UserController userController = new UserController();
        var result = userController.GetByEmail(GeneralTestingMethods.random_string(20) + "@example.com");
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    // 15. Test that retrieving a user with invalid phone returns NotFound
    [TestMethod]
    public void GetUserByPhoneTest_NotFound()
    {
        UserController userController = new UserController();
        var result = userController.GetByPhone(GeneralTestingMethods.random_string(20));
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }
}