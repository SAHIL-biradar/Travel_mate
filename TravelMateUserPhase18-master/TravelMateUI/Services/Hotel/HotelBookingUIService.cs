using System.Net.Http.Json;
using TravelMate.Models;
using TravelMate2.Services;
using TravelMateUI;


namespace TravelMate2.Services
{
    public interface IHotelBookingUIService
    {
        Task<List<HotelBooking>> GetAllBookings(int currentUserId);
        Task<HotelBooking> GetBookingById(int id, int currentUserId);
        Task AddNewBooking(HotelBooking booking, int currentUserId);
        Task DeleteBooking(int id, int currentUserId);
    }

    public class HotelBookingUIService : IHotelBookingUIService
    {
        private readonly HttpClient _httpClient;

        public HotelBookingUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Program.Configuration["HotelBooking"]!);

        }

        public async Task<List<HotelBooking>> GetAllBookings(int currentUserId)
        {
            return await _httpClient.GetFromJsonAsync<List<HotelBooking>>($"all?currentUserId={currentUserId}");
        }

        public async Task<HotelBooking> GetBookingById(int id, int currentUserId)
        {
            var response = await _httpClient.GetAsync($"{id}?currentUserId={currentUserId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HotelBooking>();
            }
            else
            {
                throw new Exception("Booking not found");
            }
        }
        
        public async Task AddNewBooking(HotelBooking booking, int currentUserId)
        {
            await _httpClient.PostAsJsonAsync($"?currentUserId={currentUserId}", booking);
        }
        
        public async Task DeleteBooking(int id, int currentUserId)
        {
            await _httpClient.DeleteAsync($"{id}?currentUserId={currentUserId}");
        }
    }
}
