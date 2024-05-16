
using ParkBlazor.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace ParkBlazor.Services
{
    public class ComputersService
    {
        private readonly HttpClient _httpClient;

        public ComputersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Computers>> GetAllComputersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Computers>>("https://localhost:7195/api/Computers");
        }

        public async Task<Computers> AddComputerAsync(Computers computer)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7195/api/Computers", computer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Computers>();
        }

        public async Task<Computers> UpdateComputerAsync(Computers computer)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7195/api/Computers/{computer.Id}", computer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Computers>();
        }

        public async Task<HttpStatusCode> DeleteComputerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7195/api/Computers/{id}");
            return response.StatusCode;
        }
    }
}