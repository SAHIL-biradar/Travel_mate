using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AdminMvcUi.Models;

namespace AdminMvcUi.Services
{
    public interface IAdminService
    {
        Task<List<User>> GetAll();
        Task DeleteUser(int id);
    }

    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5254/api/");
        }

        public async Task<List<User>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("Admin/GetAllUsers");
        }

        public async Task DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"Admin/Delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error deleting user. Please try again.");
            }
        }
    }
}
