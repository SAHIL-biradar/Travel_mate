
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelRatingDll.Models;
using HotelRatingDll.Services;

namespace HotelRatingwebapi.DataServices
{
    public interface IHotelRatingDataService
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

    public class HotelRatingDataService : IHotelRatingDataService
    {
        private readonly IHotelRatingService _hotelRatingService;

        public HotelRatingDataService(IHotelRatingService hotelRatingService)
        {
            _hotelRatingService = hotelRatingService;
        }

        public Task AddHotelRatingAsync(HotelRating hotelRating, int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.AddHotelRating(hotelRating, currentUserId));
        }

        public Task UpdateHotelRatingAsync(HotelRating hotelRating, int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.UpdateHotelRating(hotelRating, currentUserId));
        }

        public Task DeleteHotelRatingAsync(int id, int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.DeleteHotelRating(id, currentUserId));
        }

        public Task<HotelRating> GetHotelRatingByIdAsync(int id, int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.GetHotelRatingById(id, currentUserId));
        }

        public Task<List<HotelRating>> GetAllHotelRatingsAsync(int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.GetAllHotelRatings(currentUserId));
        }

        public Task AddReviewAsync(int ratingId, string reviewText, int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.AddReview(ratingId, reviewText, currentUserId));
        }

        public Task UpdateReviewAsync(int ratingId, string reviewText, int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.UpdateReview(ratingId, reviewText, currentUserId));
        }

        public Task DeleteReviewAsync(int ratingId, int currentUserId)
        {
            return Task.Run(() => _hotelRatingService.DeleteReview(ratingId, currentUserId));
        }
    }
}
