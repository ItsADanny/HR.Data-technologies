using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.Controller;
using PCWeb_Backend.DTO;
using Microsoft.Extensions.Configuration;

namespace PCWeb_API_Tests;

[TestClass]
public sealed class API_Tests_Compatibility : BaseTest
{
    // ====================================================================================
    // TEST DATA
    // ====================================================================================

    // Existing test product IDs (should be set to valid products in the database)
    public static int test_cpu_ID_alreadyInDB = TestConfiguration.PRODUCT_ID;
    public static int test_motherboard_ID_alreadyInDB = 2;
    public static int test_memory_ID_alreadyInDB = 3;
    public static int test_gpu_ID_alreadyInDB = 4;
    public static int test_case_ID_alreadyInDB = 5;
    public static int test_cooler_ID_alreadyInDB = 6;
    public static int test_psu_ID_alreadyInDB = 7;

    // ====================================================================================
    // HELPERS
    // ====================================================================================

    private CompatibilityCheckRequest CreateValidCompatibilityRequest()
    {
        return new CompatibilityCheckRequest
        {
            ProductId1 = test_cpu_ID_alreadyInDB,
            ProductId2 = test_motherboard_ID_alreadyInDB
        };
    }

    private CompatibilityCheckBatchRequest CreateValidBatchRequest()
    {
        return new CompatibilityCheckBatchRequest
        {
            SelectedParts = new Dictionary<string, int>
            {
                { "CPU", test_cpu_ID_alreadyInDB },
                { "GPU", test_gpu_ID_alreadyInDB },
                { "Power Supply", test_psu_ID_alreadyInDB }
            }
        };
    }

    // ====================================================================================
    // CPU - MOTHERBOARD COMPATIBILITY TESTS
    // ====================================================================================

    // 1. Test CPU-Motherboard compatibility with valid products
    [TestMethod]
    public void CheckCpuMotherboardCompatibilityTest_Success()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = CreateValidCompatibilityRequest();
        var result = controller.CheckCpuMotherboardCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 2. Test CPU-Motherboard compatibility with invalid product IDs
    [TestMethod]
    public void CheckCpuMotherboardCompatibilityTest_InvalidProductIds()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = new CompatibilityCheckRequest
        {
            ProductId1 = 0,
            ProductId2 = test_motherboard_ID_alreadyInDB
        };
        var result = controller.CheckCpuMotherboardCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 3. Test CPU-Motherboard compatibility with non-existent products
    // [TestMethod]
    // public void CheckCpuMotherboardCompatibilityTest_ProductNotFound()
    // {
    //     CompatibilityController controller = new CompatibilityController();
    //     CompatibilityCheckRequest request = new CompatibilityCheckRequest
    //     {
    //         ProductId1 = int.MaxValue,
    //         ProductId2 = test_motherboard_ID_alreadyInDB
    //     };
    //     var result = controller.CheckCpuMotherboardCompatibility(request);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }

    // ====================================================================================
    // MEMORY - MOTHERBOARD COMPATIBILITY TESTS
    // ====================================================================================

    // 4. Test Memory-Motherboard compatibility with valid products
    [TestMethod]
    public void CheckMemoryMotherboardCompatibilityTest_Success()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = new CompatibilityCheckRequest
        {
            ProductId1 = test_memory_ID_alreadyInDB,
            ProductId2 = test_motherboard_ID_alreadyInDB
        };
        var result = controller.CheckMemoryMotherboardCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 5. Test Memory-Motherboard compatibility with invalid product IDs
    [TestMethod]
    public void CheckMemoryMotherboardCompatibilityTest_InvalidProductIds()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = new CompatibilityCheckRequest
        {
            ProductId1 = 0,
            ProductId2 = test_motherboard_ID_alreadyInDB
        };
        var result = controller.CheckMemoryMotherboardCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 6. Test Memory-Motherboard compatibility with non-existent products
    // [TestMethod]
    // public void CheckMemoryMotherboardCompatibilityTest_ProductNotFound()
    // {
    //     CompatibilityController controller = new CompatibilityController();
    //     CompatibilityCheckRequest request = new CompatibilityCheckRequest
    //     {
    //         ProductId1 = int.MaxValue,
    //         ProductId2 = test_motherboard_ID_alreadyInDB
    //     };
    //     var result = controller.CheckMemoryMotherboardCompatibility(request);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }

    // ====================================================================================
    // GPU - CASE COMPATIBILITY TESTS
    // ====================================================================================

    // 7. Test GPU-Case compatibility with valid products
    [TestMethod]
    public void CheckGpuCaseCompatibilityTest_Success()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = new CompatibilityCheckRequest
        {
            ProductId1 = test_gpu_ID_alreadyInDB,
            ProductId2 = test_case_ID_alreadyInDB
        };
        var result = controller.CheckGpuCaseCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 8. Test GPU-Case compatibility with invalid product IDs
    [TestMethod]
    public void CheckGpuCaseCompatibilityTest_InvalidProductIds()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = new CompatibilityCheckRequest
        {
            ProductId1 = 0,
            ProductId2 = test_case_ID_alreadyInDB
        };
        var result = controller.CheckGpuCaseCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 9. Test GPU-Case compatibility with non-existent products
    // [TestMethod]
    // public void CheckGpuCaseCompatibilityTest_ProductNotFound()
    // {
    //     CompatibilityController controller = new CompatibilityController();
    //     CompatibilityCheckRequest request = new CompatibilityCheckRequest
    //     {
    //         ProductId1 = int.MaxValue,
    //         ProductId2 = test_case_ID_alreadyInDB
    //     };
    //     var result = controller.CheckGpuCaseCompatibility(request);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }

    // ====================================================================================
    // CPU COOLER - CPU COMPATIBILITY TESTS
    // ====================================================================================

    // 10. Test CPU Cooler-CPU compatibility with valid products
    [TestMethod]
    public void CheckCoolerCpuCompatibilityTest_Success()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = new CompatibilityCheckRequest
        {
            ProductId1 = test_cooler_ID_alreadyInDB,
            ProductId2 = test_cpu_ID_alreadyInDB
        };
        var result = controller.CheckCoolerCpuCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 11. Test CPU Cooler-CPU compatibility with invalid product IDs
    [TestMethod]
    public void CheckCoolerCpuCompatibilityTest_InvalidProductIds()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckRequest request = new CompatibilityCheckRequest
        {
            ProductId1 = 0,
            ProductId2 = test_cpu_ID_alreadyInDB
        };
        var result = controller.CheckCoolerCpuCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 12. Test CPU Cooler-CPU compatibility with non-existent products
    // [TestMethod]
    // public void CheckCoolerCpuCompatibilityTest_ProductNotFound()
    // {
    //     CompatibilityController controller = new CompatibilityController();
    //     CompatibilityCheckRequest request = new CompatibilityCheckRequest
    //     {
    //         ProductId1 = int.MaxValue,
    //         ProductId2 = test_cpu_ID_alreadyInDB
    //     };
    //     var result = controller.CheckCoolerCpuCompatibility(request);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }

    // ====================================================================================
    // SYSTEM PSU COMPATIBILITY TESTS
    // ====================================================================================

    // 13. Test System-PSU compatibility with valid products
    [TestMethod]
    public void CheckSystemPsuCompatibilityTest_Success()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckBatchRequest request = CreateValidBatchRequest();
        var result = controller.CheckSystemPsuCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 14. Test System-PSU compatibility with no parts
    [TestMethod]
    public void CheckSystemPsuCompatibilityTest_NoParts()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckBatchRequest request = new CompatibilityCheckBatchRequest
        {
            SelectedParts = new Dictionary<string, int>()
        };
        var result = controller.CheckSystemPsuCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 15. Test System-PSU compatibility with null parts
    [TestMethod]
    public void CheckSystemPsuCompatibilityTest_NullParts()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckBatchRequest request = new CompatibilityCheckBatchRequest
        {
            SelectedParts = null
        };
        var result = controller.CheckSystemPsuCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    // 16. Test System-PSU compatibility with no PSU selected
    [TestMethod]
    public void CheckSystemPsuCompatibilityTest_NoPsu()
    {
        CompatibilityController controller = new CompatibilityController();
        CompatibilityCheckBatchRequest request = new CompatibilityCheckBatchRequest
        {
            SelectedParts = new Dictionary<string, int>
            {
                { "CPU", test_cpu_ID_alreadyInDB },
                { "GPU", test_gpu_ID_alreadyInDB }
            }
        };
        var result = controller.CheckSystemPsuCompatibility(request);
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    // 17. Test System-PSU compatibility with non-existent PSU
    // [TestMethod]
    // public void CheckSystemPsuCompatibilityTest_PSUNotFound()
    // {
    //     CompatibilityController controller = new CompatibilityController();
    //     CompatibilityCheckBatchRequest request = new CompatibilityCheckBatchRequest
    //     {
    //         SelectedParts = new Dictionary<string, int>
    //         {
    //             { "CPU", test_cpu_ID_alreadyInDB },
    //             { "GPU", test_gpu_ID_alreadyInDB },
    //             { "Power Supply", int.MaxValue }
    //         }
    //     };
    //     var result = controller.CheckSystemPsuCompatibility(request);
    //     Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    // }
}