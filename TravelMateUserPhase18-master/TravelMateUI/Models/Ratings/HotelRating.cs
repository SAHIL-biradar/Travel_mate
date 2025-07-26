namespace TravelMateUI.Models.Ratings
{
    public class HotelRating
    {
        public int HotelRatingId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}
