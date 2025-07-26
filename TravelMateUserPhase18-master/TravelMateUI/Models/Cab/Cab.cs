using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Models
{
    public class LocationResult
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
    public enum VehicleType
    {
        Mini,
        SUV,
        Prime,
        Sedan
    }
    public class Cab
    {
        public int CabId { get; set; }
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Vehicle Name is required")]
        [StringLength(100, ErrorMessage = "Vehicle Name can't be longer than 100 characters")]
        public string VehicleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration Number is required")]
        [StringLength(50, ErrorMessage = "Registration Number can't be longer than 50 characters")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "License Number is required")]
        [MinLength(16, ErrorMessage = "License Number can't be longer than 50 characters")]
        public string LicenseNumber { get; set; } = string.Empty;

        public string CabPhoto { get; set; } = string.Empty;

        public string DriverPhoto { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool AvailabilityStatus { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required")]
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "Number of Seats is required")]
        [Range(1, 7, ErrorMessage = "Number of Seats must be between 1 and 7")]
        public int NumberOfSeats { get; set; }

        [Required(ErrorMessage = "Price Per Km is required")]
        [Range(0.1, 1000, ErrorMessage = "Price Per Km must be between 0.1 and 1000")]
        public decimal PricePerKm { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public decimal Rating { get; set; }
    }
}
