using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;

namespace PCWeb_API_Tests;

[TestClass]
public sealed class API_Tests_UserSession : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    // Existing test user ID (should be set to a valid user in the database)
    public static int test_user_ID_alreadyInDB = TestConfiguration.USER_ID;

    // ====================================================================================
    // GET TESTS
    // ====================================================================================

    // 1. Test that a session can be retrieved by valid user ID (controller returns Ok())
    // [TestMethod]
    // public void GetSessionByUserIdTest_ReturnsOk()
    // {
    //     UserSessionController controller = new UserSessionController();
    //     var result = controller.GetByUserID(test_user_ID_alreadyInDB);
    //     Assert.IsInstanceOfType(result, typeof(OkResult));
    // }

    // 2. Test that session retrieval with invalid token returns NotFound
    [TestMethod]
    public void GetSessionByTokenTest_SessionNotFound()
    {
        UserSessionController controller = new UserSessionController();
        string sessionToken = "invalid-token-" + Guid.NewGuid().ToString("N");
        var getResult = controller.GetByUserSession(sessionToken);
        Assert.IsInstanceOfType(getResult.Result, typeof(NotFoundObjectResult));
    }

    // 3. Test that session retrieval with null token returns NotFound
    [TestMethod]
    public void GetSessionByTokenTest_NullToken()
    {
        UserSessionController controller = new UserSessionController();
        var result = controller.GetByUserSession(null!);
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    // 4. Test that session retrieval with empty token returns NotFound
    [TestMethod]
    public void GetSessionByTokenTest_EmptyToken()
    {
        UserSessionController controller = new UserSessionController();
        var result = controller.GetByUserSession("");
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    // ====================================================================================
    // POST TESTS
    // ====================================================================================

    // 5. Test that a new session can be created (controller returns Ok())
    // [TestMethod]
    // public void CreateUserSessionTest_Success()
    // {
    //     UserSessionController controller = new UserSessionController();
    //     UserSession newSession = new UserSession(test_user_ID_alreadyInDB);
    //     var result = controller.CreateUserSession(newSession);
    //     Assert.IsInstanceOfType(result, typeof(OkResult));
    // }

    // ====================================================================================
    // DELETE TESTS
    // ====================================================================================

    // 6. Test that a session can be deleted (controller returns Ok())
    [TestMethod]
    public void DeleteUserSessionTest_Success()
    {
        UserSessionController controller = new UserSessionController();
        var result = controller.DeleteUserSession(Guid.NewGuid().ToString("N"));
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    // 7. Test that deleting with empty token returns Ok (controller returns Ok())
    [TestMethod]
    public void DeleteUserSessionTest_EmptyToken()
    {
        UserSessionController controller = new UserSessionController();
        var result = controller.DeleteUserSession("");
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}
