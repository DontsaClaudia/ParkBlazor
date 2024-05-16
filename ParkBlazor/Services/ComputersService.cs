
using ParkBlazor.Models;
using System.Net.Http.Json;

namespace ParkBlazor.Services
{
    public class ComputersService
    {
        private readonly HttpClient _httpClient;

        public ComputersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Computers>> List()
        {
            return await _httpClient.GetFromJsonAsync<List<Computers>>("https://localhost:7195/api/Computers");
        }

        public async Task<Computers> Add(Computers computer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Computers", computer);
            return await response.Content.ReadFromJsonAsync<Computers>();
        }

        public async Task<Computers> Update(Computers computer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Computers/{computer.Id}", computer);
            return await response.Content.ReadFromJsonAsync<Computers>();
        }

        public async Task<System.Net.HttpStatusCode> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Computers/{id}");
            return response.StatusCode;
        }
    }
}