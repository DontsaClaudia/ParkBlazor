using ParkBlazor.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;


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
            return await _httpClient.GetFromJsonAsync<List<Computers>>("https://localhost:7227/api/Computers");
        }

        public async Task<Computers> GetComputerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Computers>($"https://localhost:7227/api/Computers/{id}");
        }

        public async Task<Computers> AddComputerAsync(Computers computer)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7227/api/Computers", computer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Computers>();
        }

        public async Task<Computers> UpdateComputerAsync(Computers computer)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7227/api/Computers/{computer.Id}", computer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Computers>();
        }

        public async Task<HttpStatusCode> DeleteComputerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7227/api/Computers/{id}");
            return response.StatusCode;
        }

        public async Task<List<Computers>> SearchComputersAsync(string manufacturer, string model, int? roomId, int? minMemory, int? maxMemory)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(manufacturer))
                queryParams.Add($"manufacturer={manufacturer}");
            if (!string.IsNullOrEmpty(model))
                queryParams.Add($"model={model}");
            if (roomId.HasValue)
                queryParams.Add($"roomId={roomId.Value}");
            if (minMemory.HasValue)
                queryParams.Add($"minMemory={minMemory.Value}");
            if (maxMemory.HasValue)
                queryParams.Add($"maxMemory={maxMemory.Value}");

            var queryString = string.Join("&", queryParams);
            return await _httpClient.GetFromJsonAsync<List<Computers>>($"https://localhost:7227/api/Computers/search?{queryString}");
        }

        public async Task<bool> ComputerExistsAsync(Computers computer)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7227/api/Computers/exists", computer);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}