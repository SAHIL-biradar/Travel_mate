using HotelRatingDll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelRatingDll.Services
{
    public interface IHotelRatingService
    {
        void AddHotelRating(HotelRating hotelRating, int currentUserId);
        void UpdateHotelRating(HotelRating hotelRating, int currentUserId);
        void DeleteHotelRating(int id, int currentUserId);
        HotelRating GetHotelRatingById(int id, int currentUserId);
        List<HotelRating> GetAllHotelRatings(int currentUserId);

        void AddReview(int ratingId, string reviewText, int currentUserId);
        void UpdateReview(int ratingId, string reviewText, int currentUserId);
        void DeleteReview(int ratingId, int currentUserId);
    }

    public class HotelRatingService : IHotelRatingService
    {
        private readonly HotelRatingDbContext _context;

        public HotelRatingService(HotelRatingDbContext context)
        {
            _context = context;
        }

        public List<HotelRating> GetAllHotelRatings(int currentUserId)
        {
            return _context.HotelRatings
                           .Where(hr => hr.UserId == currentUserId)
                           .ToList();
        }

        public HotelRating GetHotelRatingById(int id, int currentUserId)
        {
            var ratingEntity = _context.HotelRatings
                                       .FirstOrDefault(hr => hr.HotelRatingId == id);

            if (ratingEntity == null || ratingEntity.UserId != currentUserId)
            {
                throw new Exception($"Rating with id {id} not found or you do not have access.");
            }

            return ratingEntity;
        }

        public void AddHotelRating(HotelRating hotelRating, int currentUserId)
        {
            var newRating = new HotelRating
            {
                UserId = currentUserId,
                HotelId = hotelRating.HotelId,
                Rating = hotelRating.Rating,
                Review = hotelRating.Review
            };

            _context.HotelRatings.Add(newRating);
            _context.SaveChanges();
        }

        public void UpdateHotelRating(HotelRating hotelRating, int currentUserId)
        {
            var existingRating = _context.HotelRatings.Find(hotelRating.HotelRatingId);

            if (existingRating == null || existingRating.UserId != currentUserId)
            {
                throw new Exception("Rating not found or you do not have access to update.");
            }

            existingRating.Rating = hotelRating.Rating;
            existingRating.Review = hotelRating.Review;

            _context.SaveChanges();
        }

        public void DeleteHotelRating(int id, int currentUserId)
        {
            var rating = _context.HotelRatings.Find(id);

            if (rating == null || rating.UserId != currentUserId)
            {
                throw new Exception("Rating not found or you do not have access to delete.");
            }

            _context.HotelRatings.Remove(rating);
            _context.SaveChanges();
        }

        public void AddReview(int ratingId, string reviewText, int currentUserId)
        {
            var ratingEntity = _context.HotelRatings.Find(ratingId);

            if (ratingEntity == null || ratingEntity.UserId != currentUserId)
            {
                throw new Exception("Rating not found or you do not have access to add a review.");
            }

            ratingEntity.Review = reviewText;
            _context.SaveChanges();
        }

        public void UpdateReview(int ratingId, string reviewText, int currentUserId)
        {
            var ratingEntity = _context.HotelRatings.Find(ratingId);

            if (ratingEntity == null || ratingEntity.UserId != currentUserId)
            {
                throw new Exception("Rating not found or you do not have access to update the review.");
            }

            ratingEntity.Review = reviewText;
            _context.SaveChanges();
        }

        public void DeleteReview(int ratingId, int currentUserId)
        {
            var ratingEntity = _context.HotelRatings.Find(ratingId);

            if (ratingEntity == null || ratingEntity.UserId != currentUserId)
            {
                throw new Exception("Rating not found or you do not have access to delete the review.");
            }

            ratingEntity.Review = string.Empty; // Mark review as deleted
            _context.SaveChanges();
        }
    }
}
