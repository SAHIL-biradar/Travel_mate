using System.Net.Http.Json;
using TravelMateUI.Models;

namespace TravelMateUI.Services
{
    public interface IWishListUIService
    {
        Task AddWishListItem(WishListTable wishList, int userId);
        Task DeleteWishListItem(int wishListId, int userId);
        Task<List<WishListTable>> GetAllWishListItems(int userId);
        Task<WishListTable> GetWishListItemById(int wishListId, int userId);
    }
    public class WishListUIService : IWishListUIService
    {
        private readonly HttpClient _httpClient;

        public WishListUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Program.Configuration["WishlistUI"]!);
        }

        public async Task<List<WishListTable>> GetAllWishListItems(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<WishListTable>>($"wishlist/AllWishListItems?currentUserId={userId}");
        }

        public async Task<WishListTable> GetWishListItemById(int wishListId, int userId)
        {
            return await _httpClient.GetFromJsonAsync<WishListTable>($"wishlist/{wishListId}?currentUserId={userId}");
        }

        public async Task AddWishListItem(WishListTable wishList, int userId)
        {
            await _httpClient.PostAsJsonAsync($"wishlist?currentUserId={userId}", wishList);
        }
        public async Task DeleteWishListItem(int wishListId, int userId)
        {
            await _httpClient.DeleteAsync($"wishlist/{wishListId}?currentUserId={userId}");
        }
    }
}
