using Microsoft.AspNetCore.Mvc;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompatibilityController : ControllerBase
    {
        // ==========
        // CPU - Motherboard
        // Implementation for CPU - Motherboard compatibility check
        // ==========

        [HttpPost("cpu-motherboard")]
        public IActionResult CheckCpuMotherboardCompatibility([FromBody] CompatibilityCheckRequest request)
        {
            if (request.ProductId1 <= 0 || request.ProductId2 <= 0)
                return BadRequest("Invalid product IDs");

            var cpuFields = Product.ReadProductByID(request.ProductId1);
            var mbFields = Product.ReadProductByID(request.ProductId2);

            if (cpuFields == null || mbFields == null)
                return NotFound("One or both products not found");

            var result = new CompatibilityResultDTO
            {
                CheckType = "CPU-Motherboard",
                Component1 = cpuFields.FirstOrDefault()?.Name ?? "CPU",
                Component2 = mbFields.FirstOrDefault()?.Name ?? "Motherboard"
            };

            // Get CPU socket from microarchitecture
            var microarchitecture = cpuFields.FirstOrDefault(f => f.FieldName == "microarchitecture")?.FieldValue;
            var cpuSocket = ConvertMicroarchitectureToSocket(microarchitecture ?? string.Empty);

            var mbSocket = mbFields.FirstOrDefault(f => f.FieldName == "socket")?.FieldValue;

            if (string.IsNullOrEmpty(cpuSocket) || string.IsNullOrEmpty(mbSocket))
            {
                result.Warnings.Add("Socket information missing");
                result.IsCompatible = false;
                return Ok(result);
            }

            result.Details["CPU Microarchitecture"] = microarchitecture ?? "Unknown";
            result.Details["CPU Socket"] = cpuSocket;
            result.Details["Motherboard Socket"] = mbSocket;

            if (cpuSocket.ToLower() != mbSocket.ToLower())
            {
                result.Warnings.Add($"Socket mismatch: CPU requires {cpuSocket}, Motherboard has {mbSocket}");
                result.IsCompatible = false;
            }
            else
            {
                result.IsCompatible = true;
            }

            return Ok(result);
        }

        private string ConvertMicroarchitectureToSocket(string microarchitecture)
        {
            return microarchitecture.ToLower() switch
            {
                // AMD Ryzen (Zen series)
                "zen" => "AM4",
                "zen+" => "AM4",
                "zen 2" => "AM4",
                "zen 3" => "AM4",
                "zen 4" => "AM5",
                "zen 5" => "AM5",
                
                // Intel (generation-based)
                "skylake" => "LGA1151",
                "kaby lake" => "LGA1151",
                "coffee lake" => "LGA1151",
                "comet lake" => "LGA1200",
                "rocket lake" => "LGA1200",
                "alder lake" => "LGA1700",
                "raptor lake" => "LGA1700",
                "meteor lake" => "LGA1700",
                
                _ => microarchitecture  // Return original if no mapping found
            };
        }

        // ====================================================================================
        // MEMORY - MOTHERBOARD COMPATIBILITY
        // ====================================================================================

        [HttpPost("memory-motherboard")]
        public IActionResult CheckMemoryMotherboardCompatibility([FromBody] CompatibilityCheckRequest request)
        {
            if (request.ProductId1 <= 0 || request.ProductId2 <= 0)
                return BadRequest("Invalid product IDs");

            var memoryFields = Product.ReadProductByID(request.ProductId1);
            var mbFields = Product.ReadProductByID(request.ProductId2);

            if (memoryFields == null || mbFields == null)
                return NotFound("One or both products not found");

            var result = new CompatibilityResultDTO
            {
                CheckType = "Memory-Motherboard",
                Component1 = memoryFields.FirstOrDefault()?.Name ?? "Memory",
                Component2 = mbFields.FirstOrDefault()?.Name ?? "Motherboard"
            };

            // Extract specifications
            var memoryModules = int.TryParse(
                memoryFields.FirstOrDefault(f => f.FieldName == "modules")?.FieldValue, 
                out var modules) ? modules : 1;

            var mbSlots = int.TryParse(
                mbFields.FirstOrDefault(f => f.FieldName == "memory_slots")?.FieldValue, 
                out var slots) ? slots : 0;

            result.Details["Memory Modules"] = memoryModules.ToString();
            result.Details["Motherboard Slots"] = mbSlots.ToString();

            if (mbSlots == 0)
            {
                result.Warnings.Add("Motherboard memory slot information not found");
                result.IsCompatible = false;
                return Ok(result);
            }

            if (memoryModules > mbSlots)
            {
                result.Warnings.Add($"Memory modules ({memoryModules}) exceeds motherboard slots ({mbSlots})");
                result.IsCompatible = false;
            }
            else
            {
                result.IsCompatible = true;
            }

            return Ok(result);
        }

        // ====================================================================================
        // GPU - CASE COMPATIBILITY (SIZE/LENGTH)
        // ====================================================================================

        [HttpPost("gpu-case")]
        public IActionResult CheckGpuCaseCompatibility([FromBody] CompatibilityCheckRequest request)
        {
            if (request.ProductId1 <= 0 || request.ProductId2 <= 0)
                return BadRequest("Invalid product IDs");

            var gpuFields = Product.ReadProductByID(request.ProductId1);
            var caseFields = Product.ReadProductByID(request.ProductId2);

            if (gpuFields == null || caseFields == null)
                return NotFound("One or both products not found");

            var result = new CompatibilityResultDTO
            {
                CheckType = "GPU-Case",
                Component1 = gpuFields.FirstOrDefault()?.Name ?? "GPU",
                Component2 = caseFields.FirstOrDefault()?.Name ?? "Case"
            };

            var gpuLength = int.TryParse(
                gpuFields.FirstOrDefault(f => f.FieldName == "length")?.FieldValue, 
                out var gpuLen) ? gpuLen : 0;

            var caseExternalVolume = double.TryParse(
                caseFields.FirstOrDefault(f => f.FieldName == "external_volume")?.FieldValue, 
                out var volume) ? volume : 0;

            result.Details["GPU Length (mm)"] = gpuLength.ToString();
            result.Details["Case External Volume (L)"] = caseExternalVolume.ToString();

            if (gpuLength == 0 || caseExternalVolume == 0)
            {
                result.Warnings.Add("GPU length or case volume information missing");
                result.IsCompatible = false;
                return Ok(result);
            }

            // Rough estimation: larger volumes typically fit longer cards
            // A case with ~50L volume should fit cards up to ~350-400mm
            double estimatedMaxLength = (caseExternalVolume / 100) * 700;

            if (gpuLength > estimatedMaxLength)
            {
                result.Warnings.Add($"GPU length ({gpuLength}mm) may not fit in case (volume: {caseExternalVolume}L)");
                result.IsCompatible = false;
            }
            else
            {
                result.IsCompatible = true;
            }

            return Ok(result);
        }

        // ====================================================================================
        // CPU COOLER - CPU COMPATIBILITY (TDP)
        // ====================================================================================

        [HttpPost("cooler-cpu")]
        public IActionResult CheckCoolerCpuCompatibility([FromBody] CompatibilityCheckRequest request)
        {
            if (request.ProductId1 <= 0 || request.ProductId2 <= 0)
                return BadRequest("Invalid product IDs");

            var coolerFields = Product.ReadProductByID(request.ProductId1);
            var cpuFields = Product.ReadProductByID(request.ProductId2);

            if (coolerFields == null || cpuFields == null)
                return NotFound("One or both products not found");

            var result = new CompatibilityResultDTO
            {
                CheckType = "Cooler-CPU",
                Component1 = coolerFields.FirstOrDefault()?.Name ?? "CPU Cooler",
                Component2 = cpuFields.FirstOrDefault()?.Name ?? "CPU"
            };

            var coolerTdp = int.TryParse(
                coolerFields.FirstOrDefault(f => f.FieldName == "tdp")?.FieldValue, 
                out var coolerTdpVal) ? coolerTdpVal : 0;

            var cpuTdp = int.TryParse(
                cpuFields.FirstOrDefault(f => f.FieldName == "tdp")?.FieldValue, 
                out var cpuTdpVal) ? cpuTdpVal : 0;

            result.Details["Cooler TDP Rating (W)"] = coolerTdp.ToString();
            result.Details["CPU TDP (W)"] = cpuTdp.ToString();

            if (coolerTdp == 0 || cpuTdp == 0)
            {
                result.Warnings.Add("TDP information missing from cooler or CPU");
                result.IsCompatible = false;
                return Ok(result);
            }

            if (cpuTdp > coolerTdp)
            {
                result.Warnings.Add($"CPU TDP ({cpuTdp}W) exceeds cooler rating ({coolerTdp}W)");
                result.IsCompatible = false;
            }
            else
            {
                result.IsCompatible = true;
            }

            return Ok(result);
        }

        // ====================================================================================
        // SYSTEM POWER - PSU COMPATIBILITY (CPU + GPU + Overhead)
        // ====================================================================================

        [HttpPost("system-psu")]
        public IActionResult CheckSystemPsuCompatibility([FromBody] CompatibilityCheckBatchRequest request)
        {
            if (request.SelectedParts == null || request.SelectedParts.Count == 0)
                return BadRequest("No parts provided");

            var result = new CompatibilityResultDTO
            {
                CheckType = "System-PSU",
                Component1 = "Complete System",
                Component2 = "Power Supply"
            };

            // Extract product IDs
            request.SelectedParts.TryGetValue("CPU", out int cpuId);
            request.SelectedParts.TryGetValue("GPU", out int gpuId);
            request.SelectedParts.TryGetValue("Power Supply", out int psuId);

            if (psuId <= 0)
            {
                result.Warnings.Add("Power Supply not selected");
                result.IsCompatible = false;
                return Ok(result);
            }

            var psuFields = Product.ReadProductByID(psuId);
            if (psuFields == null)
                return NotFound("PSU not found");

            var psuWattage = int.TryParse(
                psuFields.FirstOrDefault(f => f.FieldName == "wattage")?.FieldValue, 
                out var wattage) ? wattage : 0;

            result.Details["PSU Wattage (W)"] = psuWattage.ToString();

            int totalPowerRequired = 0;

            // CPU Power
            if (cpuId > 0)
            {
                var cpuFields = Product.ReadProductByID(cpuId);
                var cpuTdp = int.TryParse(
                    cpuFields?.FirstOrDefault(f => f.FieldName == "tdp")?.FieldValue, 
                    out var cpuTdpVal) ? cpuTdpVal : 0;
                totalPowerRequired += cpuTdp;
                result.Details["CPU TDP (W)"] = cpuTdp.ToString();
            }

            // GPU Power
            if (gpuId > 0)
            {
                var gpuFields = Product.ReadProductByID(gpuId);
                // GPU might have power_consumption or require parsing from other specs
                var gpuPower = int.TryParse(
                    gpuFields?.FirstOrDefault(f => f.FieldName == "tdp" || f.FieldName == "power_consumption")?.FieldValue, 
                    out var gpuPowerVal) ? gpuPowerVal : 100; // Default 100W if not specified
                totalPowerRequired += gpuPower;
                result.Details["GPU Power (W)"] = gpuPower.ToString();
            }

            // Add 30% headroom for system overhead and efficiency
            int totalWithOverhead = (int)(totalPowerRequired * 1.3);
            result.Details["Total Required (with 30% overhead)"] = totalWithOverhead.ToString();

            if (totalWithOverhead > psuWattage)
            {
                result.Warnings.Add($"System power ({totalWithOverhead}W) exceeds PSU capacity ({psuWattage}W)");
                result.IsCompatible = false;
            }
            else
            {
                result.IsCompatible = true;
            }

            return Ok(result);
        }

        // ====================================================================================
        // GPU - PSU COMPATIBILITY (Power connector requirements)
        // ====================================================================================

        [HttpPost("gpu-psu")]
        public IActionResult CheckGpuPsuCompatibility([FromBody] CompatibilityCheckRequest request)
        {
            if (request.ProductId1 <= 0 || request.ProductId2 <= 0)
                return BadRequest("Invalid product IDs");

            var gpuFields = Product.ReadProductByID(request.ProductId1);
            var psuFields = Product.ReadProductByID(request.ProductId2);

            if (gpuFields == null || psuFields == null)
                return NotFound("One or both products not found");

            var result = new CompatibilityResultDTO
            {
                CheckType = "GPU-PSU",
                Component1 = gpuFields.FirstOrDefault()?.Name ?? "GPU",
                Component2 = psuFields.FirstOrDefault()?.Name ?? "PSU"
            };

            // Extract modular info
            var psuModular = psuFields.FirstOrDefault(f => f.FieldName == "modular")?.FieldValue;
            var psuType = psuFields.FirstOrDefault(f => f.FieldName == "type")?.FieldValue;

            result.Details["PSU Type"] = psuType ?? "Standard";
            result.Details["PSU Modular"] = psuModular ?? "Unknown";

            // Check if PSU is modular (allows more flexibility for cable management)
            bool isModular = psuModular?.ToLower() == "true" || psuModular?.ToLower() == "yes";

            if (!isModular && psuType?.ToLower() == "standard")
            {
                result.Warnings.Add("Non-modular PSU may have cable management constraints for large GPUs");
                result.IsCompatible = true; // Still compatible but warning
            }
            else
            {
                result.IsCompatible = true;
            }

            return Ok(result);
        }

        // ====================================================================================
        // MOTHERBOARD - CASE COMPATIBILITY (Form Factor/External Volume)
        // ====================================================================================
        [HttpPost("motherboard-case")]
        public IActionResult CheckMotherboardCaseCompatibility([FromBody] CompatibilityCheckRequest request)
        {
            if (request.ProductId1 <= 0 || request.ProductId2 <= 0)
                return BadRequest("Invalid product IDs");

            var mbFields = Product.ReadProductByID(request.ProductId1);
            var caseFields = Product.ReadProductByID(request.ProductId2);

            if (mbFields == null || caseFields == null)
                return NotFound("One or both products not found");

            var result = new CompatibilityResultDTO
            {
                CheckType = "Motherboard-Case",
                Component1 = mbFields.FirstOrDefault()?.Name ?? "Motherboard",
                Component2 = caseFields.FirstOrDefault()?.Name ?? "Case"
            };

            var mbFormFactor = mbFields.FirstOrDefault(f => f.FieldName == "form_factor")?.FieldValue;
            var caseVolumeStr = caseFields.FirstOrDefault(f => f.FieldName == "external_volume")?.FieldValue;
            

            if (string.IsNullOrEmpty(mbFormFactor) || string.IsNullOrEmpty(caseVolumeStr))
            {
                result.Warnings.Add("Form factor or volume information missing from motherboard or case");
                result.IsCompatible = false;
                return Ok(result);
            }

            if (!double.TryParse(caseVolumeStr, out var caseVolume))
            {
                result.Warnings.Add("Invalid case volume information");
                result.IsCompatible = false;
                return Ok(result);
            }
            result.IsCompatible = IsMotherboardCaseCompatible(mbFormFactor, caseVolume);

            result.Details["formFactor"] = mbFormFactor;
            result.Details["caseVolume"] = caseVolume.ToString();

            return Ok(result);
        }
        private bool IsMotherboardCaseCompatible(string formFactor, double caseVolume)
        {
            switch (formFactor.ToLower())
            {
                case "atx":
                    return caseVolume >= 30;
                case "micro atx":
                    return caseVolume >= 20;
                case "mini itx":
                    return caseVolume >= 10;
                default:
                    return false;
            }
        }

        // ====================================================================================
        // CPU Cooler - Case Compatibility (Height)
        // ====================================================================================
        [HttpPost("cpu-cooler-case")]
        public IActionResult CheckCpuCoolerCaseCompatibility([FromBody] CompatibilityCheckRequest request)
        {
            if (request.ProductId1 <= 0 || request.ProductId2 <= 0)
                return BadRequest("Invalid product IDs");

            var coolerFields = Product.ReadProductByID(request.ProductId1);
            var caseFields = Product.ReadProductByID(request.ProductId2);

            if (coolerFields == null || caseFields == null)
                return NotFound("One or both products not found");

            var result = new CompatibilityResultDTO
            {
                CheckType = "CPU Cooler-Case",
                Component1 = coolerFields.FirstOrDefault()?.Name ?? "CPU Cooler",
                Component2 = caseFields.FirstOrDefault()?.Name ?? "Case"
            };

            var coolerHeightStr = coolerFields.FirstOrDefault(f => f.FieldName == "size")?.FieldValue;
            var caseHeightStr = caseFields.FirstOrDefault(f => f.FieldName == "external_volume")?.FieldValue;

            if (string.IsNullOrEmpty(coolerHeightStr) || string.IsNullOrEmpty(caseHeightStr))
            {
                result.Warnings.Add("Height information missing from CPU cooler or case");
                result.IsCompatible = false;
                return Ok(result);
            }

            if (!double.TryParse(coolerHeightStr, out var coolerHeight) || !double.TryParse(caseHeightStr, out var caseHeight))
            {
                result.Warnings.Add("Invalid height information");
                result.IsCompatible = false;
                return Ok(result);
            }

            result.IsCompatible = IsCpuCoolerCaseCompatible(coolerHeight, caseHeight);

            result.Details["coolerHeight"] = coolerHeight.ToString();
            result.Details["caseHeight"] = caseHeight.ToString();

            return Ok(result);
        }

        private bool IsCpuCoolerCaseCompatible(double coolerHeight, double caseHeight)
        {
            // Rough estimation: cases with larger volume typically support taller coolers
            // A case with ~30L volume should support coolers up to ~160mm
            double estimatedMaxCoolerHeight = (caseHeight / 100) * 160;
            return coolerHeight <= estimatedMaxCoolerHeight;
        }


    } 
}