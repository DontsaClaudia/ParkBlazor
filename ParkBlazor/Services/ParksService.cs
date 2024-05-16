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
        public async Task<List<Parks>> GetAllParksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Parks>>("api/parks");
        }

        // Méthode pour ajouter un nouveau parc
        public async Task<Parks> AddParkAsync(Parks park)
        {
            var response = await _httpClient.PostAsJsonAsync("api/parks", park);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Parks>();
        }

        // Méthode pour mettre à jour un parc existant
        public async Task<Parks> UpdateParkAsync(Parks park)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/parks/{park.Id}", park);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Parks>();
        }

        // Méthode pour supprimer un parc
        public async Task<HttpStatusCode> DeleteParkAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/parks/{id}");
            return response.StatusCode;
        }
    }
}