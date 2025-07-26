using PackageDll.Models;
using PackageDll.Services;

namespace ProPackages.DataServices
{
    public class PackageproDataService
    {
        private readonly IPackageproServices _packageproServices;

        public PackageproDataService(IPackageproServices packageproServices)
        {
            _packageproServices = packageproServices;
        }

        public Task<List<PackagePro>> GetAllPackagesAsync()
        {
            return Task.Run(() => _packageproServices.GetAll());
        }

        public Task<PackagePro> GetPackageAsync(int packageId)
        {
            return Task.Run(() => _packageproServices.Get(packageId));
        }

        public Task AddPackageAsync(PackagePro package)
        {
            return Task.Run(() => _packageproServices.Add(package));
        }

        public Task UpdatePackageAsync(int packageId, PackagePro package)
        {
            return Task.Run(() => _packageproServices.Update(packageId, package));
        }

        public Task DeletePackageAsync(int packageId)
        {
            return Task.Run(() => _packageproServices.Delete(packageId));
        }
    }
}

