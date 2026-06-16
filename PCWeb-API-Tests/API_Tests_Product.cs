namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;
using Microsoft.Extensions.Configuration;

[TestClass]
public sealed class API_Tests_Product
{
    // ====================================================================================
    // SETUP
    // ====================================================================================

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

    [TestMethod]
    public void GetProductTest()
    {
        
    }
}