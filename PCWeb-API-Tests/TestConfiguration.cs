using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

namespace PCWeb_API_Tests;

[TestClass]
public static class TestConfiguration
{
    /// <summary>
    /// Existing test user email (set in appsettings.json or environment variables)
    /// </summary>
    public static string USER_EMAIL { get; } = GetSetting("TestAccount", "Email", "");

    /// <summary>
    /// Existing test user password (set in appsettings.json or environment variables)
    /// </summary>
    public static string USER_PASSWORD { get; } = GetSetting("TestAccount", "Password", "");

    /// <summary>
    /// Existing test user ID (default 1, override in config if needed)
    /// </summary>
    public static int USER_ID { get; } = int.TryParse(GetSetting("TestAccount", "Id", "1"), out var id) ? id : 1;

    /// <summary>
    /// Existing test address ID (default 1, override in config if needed)
    /// </summary>
    public static int ADDRESS_ID { get; } = int.TryParse(GetSetting("TestAddress", "Id", "1"), out var aId) ? aId : 1;

    /// <summary>
    /// Existing test category ID (default 1, override in config if needed)
    /// </summary>
    public static int CATEGORY_ID { get; } = int.TryParse(GetSetting("TestCategory", "Id", "1"), out var cId) ? cId : 1;

    /// <summary>
    /// Existing test product ID (default 1, override in config if needed)
    /// </summary>
    public static int PRODUCT_ID { get; } = int.TryParse(GetSetting("TestProduct", "Id", "1"), out var pId) ? pId : 1;

    /// <summary>
    /// Existing test user role ID (default 1, override in config if needed)
    /// </summary>
    public static int USER_ROLE_ID { get; } = int.TryParse(GetSetting("TestUserRole", "Id", "1"), out var rId) ? rId : 1;

    private static string GetSetting(string section, string key, string defaultValue)
    {
        try
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            return config.GetSection(section)[key] ?? defaultValue;
        }
        catch
        {
            return defaultValue;
        }
    }
}
