using HotelDll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDll.Models;

namespace HotelRatingDll.Models
{
    [Table("tblhotelRating")]
    public class HotelRating
    {
        [Key]

        public int HotelRatingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
       
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public int Rating {  get; set; }

        public string Review { get; set; } = string.Empty;



    }

    public class HotelRatingDbContext : DbContext
    {
        public HotelRatingDbContext() { }
        public HotelRatingDbContext(DbContextOptions<HotelRatingDbContext> options) : base(options)
        {

        }

        public DbSet<HotelRating> HotelRatings { get; set; }

    }
}
