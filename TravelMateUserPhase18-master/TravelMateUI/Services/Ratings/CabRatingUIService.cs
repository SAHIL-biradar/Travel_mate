using System.Net.Http.Json;
using TravelMateUI.Models.Ratings;

namespace TravelMateUI.Services.Ratings
{
    public interface ICabRatingUIService
    {
        Task AddCabRating(CabRating rating, int currentUserId);
        Task DeleteCabRating(int id, int currentUserId);
        Task<List<CabRating>> GetAllCabRatings(int currentUserId);
        Task<CabRating> GetCabRatingById(int id, int currentUserId);
        Task<List<CabRating>> GetCabRatingsByUser(int userId);
        Task<double> GetTotalRatingsForCabAsync(int cabId);
        Task UpdateCabRating(CabRating rating, int currentUserId);
    }

    public class CabRatingUIService : ICabRatingUIService
    {
        private readonly HttpClient _httpClient;

        public CabRatingUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Program.Configuration["CabRating"]!);
        }

        public async Task<List<CabRating>> GetAllCabRatings(int currentUserId)
        {
            return await _httpClient.GetFromJsonAsync<List<CabRating>>($"?currentUserId={currentUserId}/");
        }

        public async Task<CabRating> GetCabRatingById(int id, int currentUserId)
        {
            var response = await _httpClient.GetAsync($"{id}/{currentUserId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CabRating>();
            }
            else
            {
                throw new Exception("Cab rating not found");
            }
        }

        public async Task AddCabRating(CabRating rating, int currentUserId)
        {
            await _httpClient.PostAsJsonAsync($"?currentUserId={currentUserId}", rating);
        }

        public async Task DeleteCabRating(int id, int currentUserId)
        {
            await _httpClient.DeleteAsync($"{id}?currentUserId={currentUserId}");
        }

        public async Task UpdateCabRating(CabRating rating, int currentUserId)
        {
            await _httpClient.PutAsJsonAsync($"?currentUserId={currentUserId}", rating);
        }

        public async Task<List<CabRating>> GetCabRatingsByUser(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<CabRating>>($"user/{userId}/ratings");
        }
        public async Task<double> GetTotalRatingsForCabAsync(int cabId)
        {
            var response = await _httpClient.GetAsync($"{cabId}/totalrating");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<double>();
            }
            else
            {
                throw new Exception("Failed to fetch the total rating.");
            }
        }
    }
}