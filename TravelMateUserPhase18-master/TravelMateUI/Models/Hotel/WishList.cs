using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMateUI.Models
{
    public class WishListTable
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public DateOnly CreatedAt { get; set; }
        public string HotelName { get; set; }
        public string HotelImage { get; set; }
    }
}
