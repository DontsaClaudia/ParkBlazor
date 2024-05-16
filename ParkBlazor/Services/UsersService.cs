using ParkBlazor.Models;
using System.Net.Http;

namespace ParkBlazor.Services
{
    public class UsersService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Users>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7195/api/Users");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Users>>();
        }
    }
}
