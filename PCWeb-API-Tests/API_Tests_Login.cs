namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;
using Microsoft.Extensions.Configuration;

[TestClass]
public sealed class API_Tests_Login
{
    // ====================================================================================
    // SETUP
    // ====================================================================================

    //Existing test account credentials (should be set to a valid account in the database for the tests to work)
    public static string test_account_Email_alreadyInDB = "";
    public static string test_account_Password_alreadyInDB = "";

    //Invalid test account credentials
    public static string test_account_InvalidEmail = GeneralTestingMethods.random_string(10) + "@example.com";
    public static string test_account_InvalidPassword = GeneralTestingMethods.random_string(10);

    public static UserDTO test_account_NewUser = new UserDTO(GeneralTestingMethods.random_name(), GeneralTestingMethods.random_name(), GeneralTestingMethods.random_email(), GeneralTestingMethods.random_string(10), GeneralTestingMethods.random_phone(), GeneralTestingMethods.random_country());
    
    [TestInitialize]
    public void Setup()
    {
        //Load user-secrets from the configuration
        var config = new ConfigurationBuilder()
            .AddUserSecrets<API_Tests_Users>()
            .Build();

        //Load database connection variables from the configuration
        var settings_MySQL = config.GetSection("MySQLDatabase");
        var settings_REDIS = config.GetSection("RedisDatabase");

        //Loading database connection variables into a DBConfig object at program launch
        DBConfig? MySQL_dbConfig = new DBConfig
        {
            HST = settings_MySQL["HST"],
            PRT = settings_MySQL["PRT"],
            USR = settings_MySQL["USR"],
            PSW = settings_MySQL["PSW"],
            DBL = settings_MySQL["DBL"]
        };
        DBConfig? Redis_dbConfig = new DBConfig
        {
            HST = settings_REDIS["HST"],
            PRT = settings_REDIS["PRT"],
            USR = settings_REDIS["USR"],
            PSW = settings_REDIS["PSW"],
            DBL = settings_REDIS["DBL"]
        };

        //If database connection variables can't be loaded exit program with error
        if (MySQL_dbConfig is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (MySQL)");
        if (Redis_dbConfig is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (Redis)");
        
        //Check if the HOST and PORT for the DBConfig are filled
        if (MySQL_dbConfig.HST is null || MySQL_dbConfig.PRT is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (MySQL)");
        if (Redis_dbConfig.HST is null || Redis_dbConfig.PRT is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (Redis)");
        
        //Set the DBConfig into the DBHelper
        DBHandler.DBConfig_MySQL = MySQL_dbConfig;
        DBHandler.DBConfig_REDIS = Redis_dbConfig;
    }

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
    [TestMethod]
    public void RegisterTest_DuplicateEmail()
    {
        UserController userController = new UserController();
        
        var result = userController.CreateUser(test_account_NewUser);
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    //LOGIN TESTS
    //1. Test that a user can log in successfully with valid credentials, that are already in the database
    [TestMethod]
    public void LoginTest()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser = new UserLoginDTO(test_account_Email_alreadyInDB, test_account_Password_alreadyInDB);

        var result = userController.LoginUser(loginUser);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    //2. Test that a user can log in successfully with valid credentials, that are created in previous tests
    [TestMethod]
    public void LoginTestPreviousTests()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser = new UserLoginDTO(test_account_NewUser.Email, test_account_NewUser.Password);

        var result = userController.LoginUser(loginUser);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    //3. Test that a user cannot log in with invalid credentials
    [TestMethod]
    public void LoginTest_InvalidCredentials()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser = new UserLoginDTO(GeneralTestingMethods.random_string(20) + "@example.com", "password123");

        var result = userController.LoginUser(loginUser);
        Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedObjectResult));
    }

    //4. Test that a user cannot log in with an empty email or password
    [TestMethod]
    public void LoginTest_EmptyValues()
    {
        UserController userController = new UserController();
        UserLoginDTO loginUser_EmptyEmail = new UserLoginDTO("", GeneralTestingMethods.random_string(10));
        UserLoginDTO loginUser_EmptyPassword = new UserLoginDTO(test_account_InvalidEmail, "");
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
        UserLoginDTO loginUser = new UserLoginDTO(test_account_InvalidEmail, GeneralTestingMethods.random_string(10));

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
