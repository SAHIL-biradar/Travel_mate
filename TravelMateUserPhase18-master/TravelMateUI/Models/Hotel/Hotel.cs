using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Models
{
  
        public class Hotel
        {
            public int HotelId { get; set; }

            public int HotelOwnerId { get; set; }

            [Required(ErrorMessage = "Hotel Name is required")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Address is required")]
            public string Address { get; set; } = string.Empty;

            [Required(ErrorMessage = "City is required")]
            public string City { get; set; } = string.Empty;

            [Required(ErrorMessage = "State is required")]
            public string State { get; set; } = string.Empty;

            [Required(ErrorMessage = "Country is required")]
            public Nationality Country { get; set; }

            [MinLength(6, ErrorMessage = "ZipCode must be at least 6 characters long")]
            public string ZipCode { get; set; } = string.Empty;

            [Required(ErrorMessage = "Description is required")]
            public string Description { get; set; } = string.Empty;

            public decimal Latitude { get; set; }

            public decimal Longitude { get; set; }

            [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
            public decimal Rating { get; set; }

            public bool AvailabilityStatus { get; set; }

            [Required(ErrorMessage = "Hotel Image is required")]
            public string HotelImage { get; set; } = string.Empty;
        }
    }
