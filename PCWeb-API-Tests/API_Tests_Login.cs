namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;

[TestClass]
public sealed class API_Tests_Login : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    // Existing test account credentials (set these to valid credentials for login tests)
    public static string test_account_Email_alreadyInDB = TestConfiguration.USER_EMAIL;
    public static string test_account_Password_alreadyInDB = TestConfiguration.USER_PASSWORD;

    public static UserDTO test_account_NewUser = new UserDTO(GeneralTestingMethods.random_name(), GeneralTestingMethods.random_name(), GeneralTestingMethods.random_email(), GeneralTestingMethods.random_string(10), GeneralTestingMethods.random_phone(), GeneralTestingMethods.random_country());

    // ====================================================================================
    // TESTS
    // ====================================================================================

    //REGISTER TESTS
    //1. Test that a new user can be registered successfully
    [TestMethod]
    public void RegisterTest()
    {
        UserController userController = new UserController();
        
        var result = userController.CreateUser(test_account_NewUser);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    //2. Test that a user cannot be registered with an existing email
    // [TestMethod]
    // public void RegisterTest_DuplicateEmail()
    // {
    //     UserController userController = new UserController();
        
    //     var result = userController.CreateUser(test_account_NewUser);
    //     Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    // }

    //LOGIN TESTS
    //1. Test that a user can log in successfully with valid credentials, that are already in the database
    // [TestMethod]
    // public void LoginTest()
    // {
    //     UserController userController = new UserController();
    //     UserLoginDTO loginUser = new UserLoginDTO(test_account_Email_alreadyInDB, test_account_Password_alreadyInDB);

    //     var result = userController.LoginUser(loginUser);
    //     Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    // }

    //2. Test that a user can log in successfully with valid credentials, that are created in previous tests
    // [TestMethod]
    // public void LoginTestPreviousTests()
    // {
    //     UserController userController = new UserController();
    //     UserLoginDTO loginUser = new UserLoginDTO(test_account_NewUser.Email, test_account_NewUser.Password);

    //     var result = userController.LoginUser(loginUser);
    //     Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    // }

    //3. Test that a user cannot log in with invalid credentials
    [TestMethod]
    public void LoginTest_InvalidCredentials()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser = new UserLoginDTO(GeneralTestingMethods.random_string(20) + "@example.com", "password123");

        var result = userController.LoginUser(loginUser);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    //4. Test that a user cannot log in with an empty email or password
    [TestMethod]
    public void LoginTest_EmptyValues()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser_EmptyEmail = new UserLoginDTO("", GeneralTestingMethods.random_string(10));
        UserLoginDTO loginUser_EmptyPassword = new UserLoginDTO(GeneralTestingMethods.random_string(10) + "@example.com", "");
        UserLoginDTO loginUser_EmptyBoth = new UserLoginDTO("", "");

        var result = userController.LoginUser(loginUser_EmptyEmail);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        result = userController.LoginUser(loginUser_EmptyPassword);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        result = userController.LoginUser(loginUser_EmptyBoth);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    //5. Test that a user cannot log in with an invalid email format
    [TestMethod]
    public void LoginTest_InvalidEmailFormat()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser = new UserLoginDTO(GeneralTestingMethods.random_string(10) + "@example.com", GeneralTestingMethods.random_string(10));

        var result = userController.LoginUser(loginUser);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    //6. Test that a user cannot log in with special characters in the email
    [TestMethod]
    public void LoginTest_SpecialCharactersInEmail()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser = new UserLoginDTO("test!@#.com", "password123");

        var result = userController.LoginUser(loginUser);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }
}
