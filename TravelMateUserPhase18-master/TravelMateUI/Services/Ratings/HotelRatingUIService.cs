using System.Collections.Generic;
using System.Net.Http.Json;
using TravelMateUI.Models.Ratings;
using System.Threading.Tasks;

namespace TravelMateUI.Services.Ratings
{
    public interface IHotelRatingUIService
    {
        Task AddHotelRatingAsync(HotelRating hotelRating, int currentUserId);
        Task AddReviewAsync(int ratingId, string reviewText, int currentUserId);
        Task DeleteHotelRatingAsync(int id, int currentUserId);
        Task DeleteReviewAsync(int ratingId, int currentUserId);
        Task<List<HotelRating>> GetAllHotelRatingsAsync(int currentUserId);
        Task<HotelRating> GetHotelRatingByIdAsync(int id, int currentUserId);
        Task UpdateHotelRatingAsync(HotelRating hotelRating, int currentUserId);
        Task UpdateReviewAsync(int ratingId, string reviewText, int currentUserId);
    }
    public class HotelRatingUIService : IHotelRatingUIService
    {
        private readonly HttpClient _httpClient;

        public HotelRatingUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5082/api/");
        }

        public async Task<HotelRating> GetHotelRatingByIdAsync(int id, int currentUserId)
        {
            return await _httpClient.GetFromJsonAsync<HotelRating>($"hotelrating/{id}?currentUserId={currentUserId}");
        }

        public async Task<List<HotelRating>> GetAllHotelRatingsAsync(int currentUserId)
        {
            return await _httpClient.GetFromJsonAsync<List<HotelRating>>($"hotelrating?currentUserId={currentUserId}");
        }

        public async Task AddHotelRatingAsync(HotelRating hotelRating, int currentUserId)
        {
            await _httpClient.PostAsJsonAsync($"hotelrating?currentUserId={currentUserId}", hotelRating);
        }

        public async Task UpdateHotelRatingAsync(HotelRating hotelRating, int currentUserId)
        {
            await _httpClient.PutAsJsonAsync($"hotelrating?currentUserId={currentUserId}", hotelRating);
        }

        public async Task DeleteHotelRatingAsync(int id, int currentUserId)
        {
            await _httpClient.DeleteAsync($"hotelrating/{id}?currentUserId={currentUserId}");
        }

        public async Task AddReviewAsync(int ratingId, string reviewText, int currentUserId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "ratingId", ratingId.ToString() },
                { "reviewText", reviewText },
                { "currentUserId", currentUserId.ToString() }
            };
            var content = new FormUrlEncodedContent(parameters);
            await _httpClient.PostAsync("hotelrating/review", content);
        }

        public async Task UpdateReviewAsync(int ratingId, string reviewText, int currentUserId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "ratingId", ratingId.ToString() },
                { "reviewText", reviewText },
                { "currentUserId", currentUserId.ToString() }
            };
            var content = new FormUrlEncodedContent(parameters);
            await _httpClient.PutAsync("hotelrating/review", content);
        }

        public async Task DeleteReviewAsync(int ratingId, int currentUserId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "ratingId", ratingId.ToString() },
                { "currentUserId", currentUserId.ToString() }
            };
            var content = new FormUrlEncodedContent(parameters);
            await _httpClient.DeleteAsync("hotelrating/review?" + await new FormUrlEncodedContent(parameters).ReadAsStringAsync());
        }
    }
}
