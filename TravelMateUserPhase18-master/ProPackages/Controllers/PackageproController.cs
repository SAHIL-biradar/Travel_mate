using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackageDll.Models;
using ProPackages.DataServices;

namespace ProPackages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class PackageController : ControllerBase
    {
        private readonly PackageproDataService _packageDataServices;

        public PackageController(PackageproDataService packageDataServices)
        {
            _packageDataServices = packageDataServices;
        }

        // GET: api/packages
        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
        {
            try
            {
                var packages = await _packageDataServices.GetAllPackagesAsync();
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/packages/{package_id}
        [HttpGet("{packageId}")]
        public async Task<IActionResult> GetPackage(int packageId)
        {
            try
            {
                var package = await _packageDataServices.GetPackageAsync(packageId);
                return Ok(package);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/packages
        [HttpPost]
        public async Task<IActionResult> AddPackage([FromBody] PackagePro package)
        {
            try
            {
                await _packageDataServices.AddPackageAsync(package);
                return Ok(new { Message = "Package added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/packages/{package_id}
        [HttpPut("{packageId}")]
        public async Task<IActionResult> UpdatePackage(int packageId, [FromBody] PackagePro package)
        {
            try
            {
                await _packageDataServices.UpdatePackageAsync(packageId, package);
                return Ok(new { Message = "Package updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/packages/{package_id}
        [HttpDelete("{packageId}")]
        public async Task<IActionResult> DeletePackage(int packageId)
        {
            try
            {
                await _packageDataServices.DeletePackageAsync(packageId);
                return Ok(new { Message = "Package deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
