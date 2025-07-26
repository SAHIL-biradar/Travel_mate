using AdminMvcUi.Models;
using AdminMvcUi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdminMvcUi.Controllers
{
    public class AdminUiController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IPackageProUIService _packageProService;

        public AdminUiController(IAdminService adminService, IPackageProUIService packageProService)
        {
            _adminService = adminService;
            _packageProService = packageProService;
        }

        // User Management Actions
        public async Task<IActionResult> Index()
        {
            var users = await _adminService.GetAll();
            return View(users);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.DeleteUser(id);
            return Ok();
        }

        // PackagePro Management Actions
        public async Task<IActionResult> PackageProIndex()
        {
            var packages = await _packageProService.GetAll();
            return View(packages);
        }

        public IActionResult Add()
        {
            return View("AddEdit", new PackagePro());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var package = await _packageProService.Get(id);
            return View("AddEdit", package);
        }

        [HttpPost]
        public async Task<IActionResult> Save(PackagePro package)
        {
            if (package.PackageId == 0)
            {
                await _packageProService.Add(package);
            }
            else
            {
                await _packageProService.Update(package);
            }
            return RedirectToAction("PackageProIndex");
        }

        [HttpDelete]
        public async Task<IActionResult> Deletepro(int id)
        {
            await _packageProService.Delete(id);
            return Ok();
        }
    }
}
//Admin