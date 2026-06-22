namespace PCWeb_API_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;

[TestClass]
public sealed class API_Tests_UserRoles : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    // Existing test user role ID (should be set to a valid role in the database for the tests to work)
    public static int test_userRole_ID_alreadyInDB = TestConfiguration.USER_ROLE_ID;

    // ====================================================================================
    // TESTS
    // ====================================================================================

    // -- GET TESTS --

    // 1. Test that a user role can be retrieved successfully by ID
    // [TestMethod]
    // public void GetUserRoleByIdTest_Success()
    // {
    //     UserRoleController controller = new UserRoleController();
    //     var result = controller.GetUserRole(test_userRole_ID_alreadyInDB);
    //     Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    // }

    // // 2. Test that retrieving a non-existent user role returns NotFound
    // [TestMethod]
    // public void GetUserRoleByIdTest_NotFound()
    // {
    //     UserRoleController controller = new UserRoleController();
    //     var result = controller.GetUserRole(int.MaxValue);
    //     Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    // }

    // // 3. Test that all user roles can be retrieved successfully
    // [TestMethod]
    // public void GetAllUserRolesTest()
    // {
    //     UserRoleController controller = new UserRoleController();
    //     var result = controller.GetAllUserRoles();
    //     Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    // }

    // // -- POST (CREATE) TESTS --

    // // 4. Test that a new user role can be created successfully
    // [TestMethod]
    // public void CreateUserRoleTest_Success()
    // {
    //     UserRoleController controller = new UserRoleController();
    //     UserRole newUserRole = GeneralTestingMethods.random_userRole();
    //     var result = controller.CreateUserRole(newUserRole);
    //     Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    // }

    // // -- PUT (UPDATE) TESTS --

    // // 5. Test that a user role can be retrieved then updated successfully
    // [TestMethod]
    // public void UpdateUserRoleTest_Success()
    // {
    //     UserRoleController controller = new UserRoleController();

    //     // First get an existing role
    //     var getResult = controller.GetUserRole(test_userRole_ID_alreadyInDB);
    //     Assert.IsInstanceOfType(getResult.Result, typeof(OkObjectResult));

    //     UserRole? existingRole = (getResult.Result as OkObjectResult)?.Value as UserRole;
    //     Assert.IsNotNull(existingRole);

    //     // Modify the role name
    //     existingRole.Name = GeneralTestingMethods.random_name() + " Updated";
    //     existingRole.Description = GeneralTestingMethods.random_string(20) + " updated description";

    //     var updateResult = controller.UpdateUserRole(existingRole);
    //     Assert.IsInstanceOfType(updateResult.Result, typeof(OkObjectResult));
    // }

    // // 6. Test that updating a non-existent user role returns BadRequest
    // [TestMethod]
    // public void UpdateUserRoleTest_NotFound()
    // {
    //     UserRoleController controller = new UserRoleController();

    //     UserRole fakeRole = GeneralTestingMethods.random_userRole();
    //     fakeRole.ID = int.MaxValue; // Non-existent ID

    //     var result = controller.UpdateUserRole(fakeRole);
    //     Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
    // }

    // // -- DELETE TESTS --

    // // 7. Test that a user role can be created and then deleted successfully
    // [TestMethod]
    // public void DeleteUserRoleTest_Success()
    // {
    //     UserRoleController controller = new UserRoleController();

    //     // Create a new role first
    //     UserRole newUserRole = GeneralTestingMethods.random_userRole();
    //     var createResult = controller.CreateUserRole(newUserRole);
    //     Assert.IsInstanceOfType(createResult.Result, typeof(OkObjectResult));

    //     // Extract created role from result
    //     UserRole? createdRole = (createResult.Result as OkObjectResult)?.Value as UserRole;
    //     Assert.IsNotNull(createdRole);
    //     Assert.IsNotNull(createdRole.ID);

    //     // Delete the created role
    //     var deleteResult = controller.DeleteUserRole(createdRole.ID.Value);
    //     Assert.IsInstanceOfType(deleteResult.Result, typeof(OkResult));
    // }

    // // 8. Test that deleting a non-existent user role returns BadRequest
    // [TestMethod]
    // public void DeleteUserRoleTest_NotFound()
    // {
    //     UserRoleController controller = new UserRoleController();
    //     var result = controller.DeleteUserRole(int.MaxValue);
    //     Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
    // }
}