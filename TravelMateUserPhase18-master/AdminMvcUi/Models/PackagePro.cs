namespace AdminMvcUi.Models
{
    public class PackagePro
    {
        public int PackageId { get; set; }

        public string PackageName { get; set; } = string.Empty;

        public string PackageDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateOnly AvailableDates { get; set; }

        public string IncludedHotels { get; set; } = string.Empty;

        public string IncludedCabs { get; set; } = string.Empty;

        public string Amenities { get; set; } = string.Empty;

    }
}
