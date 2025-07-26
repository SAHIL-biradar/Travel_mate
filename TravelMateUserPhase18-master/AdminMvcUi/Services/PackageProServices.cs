using AdminMvcUi.Models;

namespace AdminMvcUi.Services
{
    public interface IPackageProUIService
    {
        Task Add(PackagePro package);
        Task Delete(int packageId);
        Task<PackagePro> Get(int packageId);
        Task<List<PackagePro>> GetAll();
        Task Update(PackagePro package);
    }

    public class PackageProUIService : IPackageProUIService
    {
        private readonly HttpClient _httpClient;

        public PackageProUIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5143/api/");
        }

        public async Task Add(PackagePro package)
        {
            await _httpClient.PostAsJsonAsync("Package", package);
        }

        public async Task<PackagePro> Get(int packageId)
        {
            var response = await _httpClient.GetAsync($"Package/{packageId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PackagePro>();
            }
            else
            {
                throw new Exception("Package not found");
            }
        }

        public async Task Update(PackagePro package)
        {
            await _httpClient.PutAsJsonAsync($"Package/{package.PackageId}", package);
        }

        public async Task Delete(int packageId)
        {
            await _httpClient.DeleteAsync($"Package/{packageId}");
        }

        public async Task<List<PackagePro>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<PackagePro>>("Package");
        }
    }
}
