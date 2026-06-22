using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

namespace PCWeb_API_Tests;

/// <summary>
/// Base class shared by all test classes to avoid duplicated Setup() code
/// </summary>
public abstract class BaseTest
{
    [TestInitialize]
    public void Setup()
    {
        // Load user-secrets from the configuration
        var config = new ConfigurationBuilder()
            .AddUserSecrets<API_Tests_Users>()
            .Build();

        // Load database connection variables from the configuration
        var settings_MySQL = config.GetSection("MySQLDatabase");
        var settings_REDIS = config.GetSection("RedisDatabase");

        // Loading database connection variables into a DBConfig object at program launch
        DBConfig? MySQL_dbConfig = new DBConfig
        {
            HST = settings_MySQL.GetValue<string>("HST"),
            PRT = settings_MySQL.GetValue<string>("PRT"),
            USR = settings_MySQL.GetValue<string>("USR"),
            PSW = settings_MySQL.GetValue<string>("PSW"),
            DBL = settings_MySQL.GetValue<string>("DBL")
        };
        DBConfig? Redis_dbConfig = new DBConfig
        {
            HST = settings_REDIS.GetValue<string>("HST"),
            PRT = settings_REDIS.GetValue<string>("PRT"),
            USR = settings_REDIS.GetValue<string>("USR"),
            PSW = settings_REDIS.GetValue<string>("PSW"),
            DBL = settings_REDIS.GetValue<string>("DBL")
        };

        // If database connection variables can't be loaded exit program with error
        if (MySQL_dbConfig is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (MySQL)");
        if (Redis_dbConfig is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (Redis)");

        // Check if the HOST and PORT for the DBConfig are filled
        if (MySQL_dbConfig.HST is null || MySQL_dbConfig.PRT is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (MySQL)");
        if (Redis_dbConfig.HST is null || Redis_dbConfig.PRT is null) throw new NullReferenceException("Can't load user-secrets into DBConfig (Redis)");

        // Set the DBConfig into the DBHelper
        DBHandler.DBConfig_MySQL = MySQL_dbConfig;
        DBHandler.DBConfig_REDIS = Redis_dbConfig;
    }
}
