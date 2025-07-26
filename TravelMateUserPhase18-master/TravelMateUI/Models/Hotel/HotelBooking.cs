using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Models
{
    public class HotelBooking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string BookingStatus { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
