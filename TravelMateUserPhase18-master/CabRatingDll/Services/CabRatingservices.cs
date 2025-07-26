using CabRatingDll.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CabRatingDll.Services
{
    public interface ICabRatingService
    {
        void AddCabRating(CabRating rating, int currentUserId);
        void AddReviewForCab(int cabId, int currentUserId, string review, int rating);
        void DeleteCabRating(int cabRatingId, int currentUserId);
        void DeleteReviewForCab(int cabRatingId, int currentUserId);
        Task<CabRating> GetCabRatingByCabIdAndUserIdAsync(int cabId, int userId);
        Task<IEnumerable<CabRating>> GetRatingsByUserAsync(int userId);
        Task<IEnumerable<CabRating>> GetRatingsForCabAsync(int cabId);
        Task<IEnumerable<string>> GetReviewsForCabAsync(int cabId);
        Task<decimal> GetTotalRatingsForCabAsync(int cabId);
        void UpdateCabRating(CabRating rating, int currentUserId);
        void UpdateReviewForCab(int cabRatingId, int currentUserId, string review, int rating);
    }

    public class CabRatingService : ICabRatingService
    {
        private readonly CabRatingDbContext _context;

        public CabRatingService(CabRatingDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalRatingsForCabAsync(int cabId)
        {
            var ratings = await _context.CabRatings
                .Where(r => r.CabId == cabId)
                .ToListAsync();

            if (ratings.Count == 0)
            {
                return 0; // No ratings available
            }

            var totalRating = ratings.Sum(r => r.Rating);
            var numberOfRatings = ratings.Count;

            // Calculate average rating
            var averageRating = totalRating / (decimal)numberOfRatings;

            return averageRating;
        }

        public async Task<IEnumerable<CabRating>> GetRatingsForCabAsync(int cabId)
        {
            return await _context.CabRatings
                .Where(r => r.CabId == cabId)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetReviewsForCabAsync(int cabId)
        {
            return await _context.CabRatings
                .Where(r => r.CabId == cabId && !string.IsNullOrEmpty(r.Review))
                .Select(r => r.Review)
                .ToListAsync();
        }

        public void AddCabRating(CabRating rating, int currentUserId)
        {
            var cabRating = new CabRating
            {
                CabId = rating.CabId,
                UserId = currentUserId,
                Review = rating.Review,
                Rating = rating.Rating
            };

            _context.CabRatings.Add(cabRating);
            _context.SaveChanges();
        }

        public void UpdateCabRating(CabRating rating, int currentUserId)
        {
            var existingRating = _context.CabRatings.FirstOrDefault(r => r.CabRatingId == rating.CabRatingId && r.UserId == currentUserId);
            if (existingRating == null)
            {
                throw new Exception("Rating not found or you do not have access to update.");
            }

            existingRating.Review = rating.Review;
            existingRating.Rating = rating.Rating;

            _context.SaveChanges();
        }

        public void AddReviewForCab(int cabId, int currentUserId, string review, int rating)
        {
            var cabRating = new CabRating
            {
                CabId = cabId,
                UserId = currentUserId,
                Review = review,
                Rating = rating
            };

            _context.CabRatings.Add(cabRating);
            _context.SaveChanges();
        }

        public void UpdateReviewForCab(int cabRatingId, int currentUserId, string review, int rating)
        {
            var existingRating = _context.CabRatings.FirstOrDefault(r => r.CabRatingId == cabRatingId && r.UserId == currentUserId);
            if (existingRating == null)
            {
                throw new Exception("Rating not found or you do not have access to update.");
            }

            existingRating.Review = review;
            existingRating.Rating = rating;

            _context.SaveChanges();
        }

        public void DeleteCabRating(int cabRatingId, int currentUserId)
        {
            var existingRating = _context.CabRatings.FirstOrDefault(r => r.CabRatingId == cabRatingId && r.UserId == currentUserId);
            if (existingRating == null)
            {
                throw new Exception("Rating not found or you do not have access to delete.");
            }

            _context.CabRatings.Remove(existingRating);
            _context.SaveChanges();
        }

        public void DeleteReviewForCab(int cabRatingId, int currentUserId)
        {
            var existingRating = _context.CabRatings.FirstOrDefault(r => r.CabRatingId == cabRatingId && r.UserId == currentUserId);
            if (existingRating == null)
            {
                throw new Exception("Rating not found or you do not have access to delete the review.");
            }

            existingRating.Review = string.Empty;
            _context.SaveChanges();
        }
        public async Task<IEnumerable<CabRating>> GetRatingsByUserAsync(int userId)
        {
            return await _context.CabRatings
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
        public async Task<CabRating> GetCabRatingByCabIdAndUserIdAsync(int cabId, int userId)
        {
            var cabRating = await _context.CabRatings
                .FirstOrDefaultAsync(r => r.CabId == cabId && r.UserId == userId);

            if (cabRating == null)
            {
                throw new Exception("Cab rating not found.");
            }

            return cabRating;
        }
    }
}