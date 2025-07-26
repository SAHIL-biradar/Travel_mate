using CabDll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDll.Models;

namespace CabRatingDll.Models
{
    [Table("tblcabRating")]
    public class CabRating
    {
        [Key]
        public int CabRatingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Cab")]
        public int CabId { get; set; }

        public int Rating {  get; set; }
        public string Review { get; set; }=string.Empty;


    }
    public class CabRatingDbContext : DbContext
    {
        public CabRatingDbContext() { }
        public CabRatingDbContext(DbContextOptions<CabRatingDbContext> options) : base(options)
        {

        }
        public DbSet<CabRating> CabRatings { get; set; }

    }
}
