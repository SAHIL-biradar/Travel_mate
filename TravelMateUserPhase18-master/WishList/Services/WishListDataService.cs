using System.Collections.Generic;
using System.Threading.Tasks;
using WishListDll.Models;
using WishListDll.Services;

namespace WishListWebApi.Services
{
    public interface IWishListDataService
    {
        Task AddWishListItem(WishListTable wishList, int currentUserId);
        Task DeleteWishListItemById(int id, int currentUserId);
        Task<WishListTable> GetWishListItemById(int id, int currentUserId);
        Task<List<WishListTable>> GetAllWishListItems(int currentUserId);
    }

    public class WishListDataService : IWishListDataService
    {
        private readonly IWishListService _wishListService;

        public WishListDataService(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        public Task<List<WishListTable>> GetAllWishListItems(int currentUserId)
        {
            return Task.Run(() => _wishListService.GetAllWishListItems(currentUserId));
        }

        public Task<WishListTable> GetWishListItemById(int id, int currentUserId)
        {
            return Task.Run(() => _wishListService.GetWishListItemById(id, currentUserId));
        }

        public Task AddWishListItem(WishListTable wishList, int currentUserId)
        {
            return Task.Run(() => _wishListService.AddNewWishListItem(wishList, currentUserId));
        }

        public Task DeleteWishListItemById(int id, int currentUserId)
        {
            return Task.Run(() => _wishListService.DeleteWishListItem(id, currentUserId));
        }
    }
}
