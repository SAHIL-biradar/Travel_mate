using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CabBookingDll.Models
{
    public enum BookingStatus
    {
        Pending,
        Accepted,
        Current,
        Finished,
        Canceled
    }
  
    [Table("Cabbookings")] // Specifies the table name in the database
    public class Cabbooking
    {
        [Key]
        public int CabbookingId { get; set; } // Primary key with auto-increment
        [ForeignKey("Users")]
        public int UserId {  get; set; }

        [ForeignKey("tblCab")]
        public int CabId { get; set; } // Foreign key referencing the Driver entity
        public string PickupLocation { get; set; }=string.Empty;


        public string DropLocation { get; set; } = string.Empty;

        public double Distance { get; set; } 

        public decimal TotalAmount { get; set; } 

        public BookingStatus BookingStatus { get; set; }        
      
    }
    public class CabBookingDbContext : DbContext
    {
        public CabBookingDbContext() { }
        public CabBookingDbContext(DbContextOptions<CabBookingDbContext> options) : base(options)
        {

        }
        public DbSet<Cabbooking> CabBookings { get; set; }

    }
}
