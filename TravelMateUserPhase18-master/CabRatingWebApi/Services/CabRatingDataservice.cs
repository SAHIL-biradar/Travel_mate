using CabRatingDll.Models;
using CabRatingDll.Services;

namespace CabRatingWebApi.DataServices
{
    public interface ICabRatingDataServices
    {
        Task AddCabRatingAsync(CabRating rating, int currentUserId);
        Task AddReviewForCabAsync(int cabId, int currentUserId, string review, int rating);
        Task DeleteCabRatingAsync(int cabRatingId, int currentUserId);
        Task DeleteReviewForCabAsync(int cabRatingId, int currentUserId);
        Task<CabRating> GetCabRatingByCabIdAndUserIdAsync(int cabId, int userId);
        Task<IEnumerable<CabRating>> GetRatingsByUserAsync(int userId);
        Task<IEnumerable<CabRating>> GetRatingsForCabAsync(int cabId);
        Task<IEnumerable<string>> GetReviewsForCabAsync(int cabId);
        Task<decimal> GetTotalRatingsForCabAsync(int cabId);
        Task UpdateCabRatingAsync(CabRating rating, int currentUserId);
        Task UpdateReviewForCabAsync(int cabRatingId, int currentUserId, string review, int rating);
    }

    public class CabRatingDataServices : ICabRatingDataServices
    {
        private readonly ICabRatingService _cabRatingService;

        public CabRatingDataServices(ICabRatingService cabRatingService)
        {
            _cabRatingService = cabRatingService;
        }

        // Asynchronous method to get total ratings for a specific cab
        public Task<decimal> GetTotalRatingsForCabAsync(int cabId)
        {
            return Task.Run(() => _cabRatingService.GetTotalRatingsForCabAsync(cabId));
        }

        // Asynchronous method to get all ratings for a specific cab
        public Task<IEnumerable<CabRating>> GetRatingsForCabAsync(int cabId)
        {
            return Task.Run(() => _cabRatingService.GetRatingsForCabAsync(cabId));
        }

        // Asynchronous method to get all reviews for a specific cab
        public Task<IEnumerable<string>> GetReviewsForCabAsync(int cabId)
        {
            return Task.Run(() => _cabRatingService.GetReviewsForCabAsync(cabId));
        }

        // Asynchronous method to add a new cab rating
        public Task AddCabRatingAsync(CabRating rating, int currentUserId)
        {
            return Task.Run(() => _cabRatingService.AddCabRating(rating, currentUserId));
        }

        // Asynchronous method to update an existing cab rating
        public Task UpdateCabRatingAsync(CabRating rating, int currentUserId)
        {
            return Task.Run(() => _cabRatingService.UpdateCabRating(rating, currentUserId));
        }

        // Asynchronous method to add a review for a specific cab
        public Task AddReviewForCabAsync(int cabId, int currentUserId, string review, int rating)
        {
            return Task.Run(() => _cabRatingService.AddReviewForCab(cabId, currentUserId, review, rating));
        }

        // Asynchronous method to update a review for a specific cab
        public Task UpdateReviewForCabAsync(int cabRatingId, int currentUserId, string review, int rating)
        {
            return Task.Run(() => _cabRatingService.UpdateReviewForCab(cabRatingId, currentUserId, review, rating));
        }

        // Asynchronous method to delete a cab rating
        public Task DeleteCabRatingAsync(int cabRatingId, int currentUserId)
        {
            return Task.Run(() => _cabRatingService.DeleteCabRating(cabRatingId, currentUserId));
        }

        // Asynchronous method to delete a review for a specific cab
        public Task DeleteReviewForCabAsync(int cabRatingId, int currentUserId)
        {
            return Task.Run(() => _cabRatingService.DeleteReviewForCab(cabRatingId, currentUserId));
        }
        public Task<IEnumerable<CabRating>> GetRatingsByUserAsync(int userId)
        {
            return _cabRatingService.GetRatingsByUserAsync(userId);
        }
        public Task<CabRating> GetCabRatingByCabIdAndUserIdAsync(int cabId, int userId)
        {
            return Task.Run(() => _cabRatingService.GetCabRatingByCabIdAndUserIdAsync(cabId, userId));
        }
    }
}