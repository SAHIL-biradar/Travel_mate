using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelMate.Models;
using TravelMateUI;


namespace TravelMate2.Services
{
    public interface ICabBookingUIService
    {
        Task<int> AddCabBooking(CabBookingModel booking, int currentUserId);
        Task UpdateCabBooking(CabBookingModel booking, BookingStatus statusModel);
        Task DeleteCabBooking(int id, int currentUserId);
        Task<List<CabBookingModel>> GetAllCabBookings(int currentUserId);
        Task<CabBookingModel> GetCabBookingById(int id, int currentUserId);
    }

    public class CabBookingUIService : ICabBookingUIService
    {
        private readonly HttpClient _httpClient;

        public CabBookingUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Program.Configuration["CabBooking"]!);
        }

        public async Task<List<CabBookingModel>> GetAllCabBookings(int currentUserId)
        {
            return await _httpClient.GetFromJsonAsync<List<CabBookingModel>>($"?currentUserId={currentUserId}");
        }

        public async Task<CabBookingModel> GetCabBookingById(int id, int currentUserId)
        {
        //http://localhost:5005/api/CabBooking/31?currentUserId=6
       // http://localhost:5005/31?currentUserId=6
            var response = await _httpClient.GetFromJsonAsync<CabBookingModel>($"{id}?currentUserId={currentUserId}");
            return response;
            //if (response.IsSuccessStatusCode)
            //{
            //    return response;
            //}
            //else
            //{
            //    throw new Exception("Cab booking not found");
            //}
        }

        public async Task<int> AddCabBooking(CabBookingModel booking, int currentUserId)
        {
            try
            {
                var response=await _httpClient.PostAsJsonAsync($"?currentUserId={currentUserId}", booking);
                if (response.IsSuccessStatusCode)
                {
                    int bookingid= await response.Content.ReadFromJsonAsync<int>();
                    return bookingid;
                }
                else
                {
                    throw new Exception("Cab booking not found");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Cab booking add failed");

            }
        }

        public async Task DeleteCabBooking(int id, int currentUserId)
        {
            await _httpClient.DeleteAsync($"{id}?currentUserId={currentUserId}");
        }

        public async Task UpdateCabBooking(CabBookingModel booking, BookingStatus statusModel)
        {
            await _httpClient.PutAsJsonAsync($"?status={(int)statusModel}", booking);
        }
    }
}
