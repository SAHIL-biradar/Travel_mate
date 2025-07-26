using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDll.Models
{
    [Table("tblPackagePro")]
    public class PackagePro
    {
        [Key]
        public int PackageId { get; set; }

        public string PackageName { get; set; } = string.Empty;

        public string PackageDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateOnly AvailableDates { get; set;}

        public string IncludedHotels { get;set;} = string.Empty;

        public string IncludedCabs { get; set; } = string.Empty;

        public string Amenities { get; set; } = string.Empty;

    }

    public class PackageProDbContext : DbContext
    {
        public PackageProDbContext() { }
        public PackageProDbContext(DbContextOptions<PackageProDbContext> options) : base(options)
        {

        }
        public DbSet<PackagePro> Packages { get; set; }

    }
}
