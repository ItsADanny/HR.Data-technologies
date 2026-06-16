namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;
using Microsoft.Extensions.Configuration;

[TestClass]
public sealed class API_Tests_Users
{
    // ====================================================================================
    // SETUP
    // ====================================================================================

    public static int test_account_ID_alreadyInDB = 1; //Existing test account ID (should be set to a valid account in the database for the tests to work)
    public static int test_account_RoleID_alreadyInDB = 1; //Existing test account Role ID (should be set to a valid account in the database for the tests to work)
    public static string test_account_Email_alreadyInDB = ""; //Existing test account Email (should be set to a valid account in the database for the tests to work)
    public static string test_account_Phone_alreadyInDB = ""; //Existing test account Phone (should be set to a valid account in the database for the tests to work)
    public static Account test_account_alreadyInDB;

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

        Console.WriteLine("MySQL Host: " + settings_MySQL["HST"]);
        Console.WriteLine("Redis Host: " + settings_REDIS["HST"]);

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

    //1. Test that a user can be retrieved successfully by ID
    [TestMethod]
    public void GetUserByIDTest()
    {
        UserController userController = new UserController();
        var result = userController.GetByUserID(test_account_ID_alreadyInDB);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        test_account_alreadyInDB = (result.Result as OkObjectResult).Value as Account;
    }

    //2. Test that a user can be retrieved successfully by Role ID
    [TestMethod]
    public void GetUserByRoleIDTest()
    {
        UserController userController = new UserController();
        var result = userController.GetByRoleID(test_account_RoleID_alreadyInDB);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    //3. Test that a user can be retrieved successfully by Email
    [TestMethod]
    public void GetUserByEmailTest()
    {
        UserController userController = new UserController();
        var result = userController.GetByEmail(test_account_Email_alreadyInDB);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

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

    //7. Test that a user can be deleted successfully
    [TestMethod]
    public void DeleteUserTest()
    {
        UserController userController = new UserController();
        UserDTO newUser = new UserDTO(GeneralTestingMethods.random_name(), GeneralTestingMethods.random_name(), GeneralTestingMethods.random_email(), GeneralTestingMethods.random_string(10), GeneralTestingMethods.random_phone(), GeneralTestingMethods.random_country());
        var createResult = userController.CreateUser(newUser);
        Assert.IsInstanceOfType(createResult.Result, typeof(OkObjectResult));
        Account createdUser = (createResult.Result as OkObjectResult).Value as Account;
        var deleteResult = userController.DeleteUserByUserID(createdUser.ID);
        Assert.IsInstanceOfType(deleteResult, typeof(OkObjectResult));
    }
}