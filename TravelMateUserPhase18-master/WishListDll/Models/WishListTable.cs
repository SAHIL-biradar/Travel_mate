using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishListDll.Models
{
    [Table("tblWishList")]
    public class WishListTable
    {
        [Key]
        public int WishListId { get; set; }

        [ForeignKey("User")]
        public int UserId {  get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public DateOnly CreatedAt { get; set; }
        [ForeignKey("Hotel")]
        public string HotelName {  get; set; }
        [ForeignKey("Hotel")]
        public string HotelImage {  get; set; }

    }
    public class WishListDbContext : DbContext
    {
        public WishListDbContext() { }
        public WishListDbContext(DbContextOptions<WishListDbContext> options) : base(options)
        {

        }

        public DbSet<WishListTable> WishLists { get; set; }
       

    }
}
