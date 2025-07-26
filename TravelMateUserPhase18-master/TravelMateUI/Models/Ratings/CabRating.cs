namespace TravelMateUI.Models.Ratings
{
    public class CabRating
    {
        public int CabRatingId { get; set; }


        public int UserId { get; set; }


        public int CabId { get; set; }

        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}
