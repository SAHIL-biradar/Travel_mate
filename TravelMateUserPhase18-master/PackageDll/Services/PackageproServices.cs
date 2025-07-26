using PackageDll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDll.Services
{
    public interface IPackageproServices
    {
        void Add(PackagePro package);
        void Delete(int packageId);
        PackagePro Get(int packageId);
        List<PackagePro> GetAll();
        void Update(int packageId, PackagePro package);
    }

    public class PackageproServices : IPackageproServices
    {
        private readonly PackageProDbContext _context;
        public PackageproServices(PackageProDbContext context)
        {
            _context = context;
        }

        public List<PackagePro> GetAll()
        {
            return _context.Packages.ToList();
        }

        public PackagePro Get(int packageId)
        {
            var packageEntity = _context.Packages.Find(packageId);

            if (packageEntity == null)
            {
                throw new Exception("Package not found.");
            }

            return packageEntity;
        }

        public void Add(PackagePro package)
        {
            if (_context.Packages.Any(p => p.PackageId == package.PackageId))
            {
                throw new Exception("Package already exists.");
            }

            _context.Packages.Add(package);
            _context.SaveChanges();
        }

        public void Update(int packageId, PackagePro package)
        {
            var packageEntity = _context.Packages.Find(packageId);

            if (packageEntity == null)
            {
                throw new Exception("Package not found.");
            }

            packageEntity.PackageName = package.PackageName;
            packageEntity.PackageDescription = package.PackageDescription;
            packageEntity.Price = package.Price;
            packageEntity.AvailableDates = package.AvailableDates;
            packageEntity.IncludedHotels = package.IncludedHotels;
            packageEntity.IncludedCabs = package.IncludedCabs;
            packageEntity.Amenities = package.Amenities;


            _context.SaveChanges();
        }

        public void Delete(int packageId)
        {
            var packageEntity = _context.Packages.Find(packageId);

            if (packageEntity == null)
            {
                throw new Exception("Package not found.");
            }

            _context.Packages.Remove(packageEntity);
            _context.SaveChanges();
        }
    }
}
