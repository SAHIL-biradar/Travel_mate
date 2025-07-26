using CabBookingDll.Models;
using CabDll.Models;
using CabDll.Services;
using HotelBookingDll.Models;
using HotelDll.Models;
using HotelDll.Services;
using SearchDll.Services;

namespace SearchWebApi.Services
{
    public interface ISearchDataService
    {
        Task<List<Hotel>> GetAllHotels();
        Task<List<Cab>> GetAllCabs();
        Task<List<Cabbooking>> GetAllCabBookings();
        Task<List<HotelBookingTable>> GetAllHotelBooking(int hotelId);
    }
    public class SearchDataService : ISearchDataService
    {
        private readonly ISearchService _searchService;

        public SearchDataService(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            // Converting synchronous method to asynchronous using Task.Run
            return await Task.Run(() => _searchService.GetAllHotels());
        }

        public async Task<List<Cab>> GetAllCabs()
        {
            // Converting synchronous method to asynchronous using Task.Run
            return await Task.Run(() => _searchService.GetAllCabs());
        }

        public async Task<List<Cabbooking>> GetAllCabBookings()
        {
            return await Task.Run(() => _searchService.GetAllCabBookings());

        }

        public async Task<List<HotelBookingTable>> GetAllHotelBooking(int hotelId)
        {
            return await Task.Run(()=> _searchService.GetAllHotelBooking(hotelId));
        }
    }
}
