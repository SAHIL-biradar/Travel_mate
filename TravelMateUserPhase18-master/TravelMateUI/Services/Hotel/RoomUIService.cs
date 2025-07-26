using System.Net.Http.Json;
using TravelMate.Models;

namespace TravelMateUI.Services
{
    public interface IRoomUIService
    {
        Task Add(Room room, int hotelId);
        Task<Room> Get(int id);
        Task Update(Room room, int id);
        Task Delete(int id);
        Task<List<Room>> GetAll(int hotelId);
        Task<List<Room>> GetAvailableRooms();
    }
    public class RoomUIService : IRoomUIService
    {
        private readonly HttpClient _httpClient;

        public RoomUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Program.Configuration["RoomUI"]!);
        }

        public async Task Add(Room room, int hotelId)
        {
            
            room.HotelId = hotelId;
            await _httpClient.PostAsJsonAsync($"rooms?hotelId={hotelId}", room);
        }

        public async Task<Room> Get(int id)
        {
            var response = await _httpClient.GetAsync($"rooms/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Room>();
            }
            else
            {
                throw new Exception("Room not found");
            }
        }

        public async Task Update(Room room, int id)
        {
            await _httpClient.PutAsJsonAsync($"rooms/{id}", room);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"rooms/{id}");
        }

        public async Task<List<Room>> GetAll(int hotelId)
        {
            return await _httpClient.GetFromJsonAsync<List<Room>>($"rooms/AllRooms?hotelId={hotelId}");
        }

        public async Task<List<Room>> GetAvailableRooms()
        {
            return await _httpClient.GetFromJsonAsync<List<Room>>("rooms/AvailableRooms");
        }
    }
}
