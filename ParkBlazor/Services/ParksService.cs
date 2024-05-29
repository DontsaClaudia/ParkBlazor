using ParkBlazor.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace ParkBlazor.Services
{
    public class ParksService
    {
        private readonly HttpClient _httpClient;

        public ParksService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Méthode pour obtenir tous les parcs
        public async Task<List<Parks?>> GetAllParksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Parks?>>("https://localhost:7227/api/Parks");
        }

        // Méthode pour ajouter un nouveau parc
        public async Task<Parks> AddParkAsync(Parks park)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7227/api/Parks", park);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Parks>();
        }

        // Méthode pour mettre à jour un parc existant
        public async Task<Parks> UpdateParkAsync(Parks park)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7227/api/Parks/{park.Id}", park);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Parks>();
        }

        // Méthode pour supprimer un parc
        public async Task<HttpStatusCode> DeleteParkAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7227/api/Parks/{id}");
            return response.StatusCode;
        }
    }
}