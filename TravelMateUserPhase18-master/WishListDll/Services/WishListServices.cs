using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WishListDll.Models;

namespace WishListDll.Services
{
    public interface IWishListService
    {
        void AddNewWishListItem(WishListTable wishList, int currentUserId);
        void DeleteWishListItem(int id, int currentUserId);
        WishListTable GetWishListItemById(int id, int currentUserId);
        List<WishListTable> GetAllWishListItems(int currentUserId);
    }

    public class WishListService : IWishListService
    {
        private readonly WishListDbContext _context;

        public WishListService(WishListDbContext context)
        {
            _context = context;
        }

        public List<WishListTable> GetAllWishListItems(int currentUserId)
        {
            return _context.WishLists
                .Where(w => w.UserId == currentUserId)
                .Select(wishListEntity => new WishListTable
                {
                    WishListId = wishListEntity.WishListId,
                    UserId = wishListEntity.UserId,
                    HotelId = wishListEntity.HotelId,
                    CreatedAt = wishListEntity.CreatedAt,
                    HotelImage = wishListEntity.HotelImage,
                    HotelName = wishListEntity.HotelName
                })
                .ToList();
        }

        public WishListTable GetWishListItemById(int id, int currentUserId)
        {
            var wishListEntity = _context.WishLists.Find(id);

            if (wishListEntity == null || wishListEntity.UserId != currentUserId)
            {
                throw new Exception($"WishList item with id {id} not found or you do not have access.");
            }

            return new WishListTable
            {
                WishListId = wishListEntity.WishListId,
                UserId = wishListEntity.UserId,
                HotelId = wishListEntity.HotelId,
                CreatedAt = wishListEntity.CreatedAt,
                HotelName= wishListEntity.HotelName,
                HotelImage= wishListEntity.HotelImage
            };
        }

        public void AddNewWishListItem(WishListTable wishList, int currentUserId)
        {
            var newWishListItem = new WishListTable
            {
                UserId = currentUserId,
                HotelId = wishList.HotelId,
                CreatedAt = wishList.CreatedAt,
                HotelImage = wishList.HotelImage,
                HotelName = wishList.HotelName
            };

            _context.WishLists.Add(newWishListItem);
            _context.SaveChanges();
        }

        public void DeleteWishListItem(int id, int currentUserId)
        {
            var wishListItem = _context.WishLists.Find(id);

            if (wishListItem == null || wishListItem.UserId != currentUserId)
            {
                throw new Exception("WishList item not found or you do not have access to delete.");
            }

            _context.WishLists.Remove(wishListItem);
            _context.SaveChanges();
        }
    }
}
