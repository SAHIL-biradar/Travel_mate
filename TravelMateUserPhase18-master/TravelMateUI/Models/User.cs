using System.ComponentModel.DataAnnotations;

namespace TravelMate.Models
{
    public enum UserType
    {
        Regular,
        hotel_owner,
        driver_client
    }
    public enum AuthProvider
    {
        Local,
        Google
    }

    public enum Nationality
    {
        IND,
        USA,
        UK,
        RUS,
        JPN
    }
	public class TokenInfo
	{
		public string token { get; set; } = string.Empty;
	}
	public class UserInfo()
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string PasswordHash { get; set; } = string.Empty;
        [Required(ErrorMessage = "Username is required")]
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nationality is required")]
        public Nationality Nationality { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [MinLength(10, ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public AuthProvider AuthProvider { get; set; } = AuthProvider.Local;

        public UserType UserType { get; set; }
    }
}