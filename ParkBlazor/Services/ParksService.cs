using ParkBlazor.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Blazored.LocalStorage;

namespace ParkBlazor.Services
{
    public class ParksService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private string _token;
        public ParksService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        private async Task InitializeAuthorizationHeader()
        {
            if (string.IsNullOrEmpty(_token))
            {
                _token = await _localStorage.GetItemAsync<string>("authToken");
            }

            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            }
        }

        public async Task SetAuthorizationHeader(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            await _localStorage.SetItemAsync("authToken", token);
        }

        // Méthode pour obtenir tous les parcs
        public async Task<List<Parks?>> GetAllParksAsync()
        {
            await InitializeAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<Parks?>>("http://localhost:5010/api/Parks");
        }

        // Méthode pour ajouter un nouveau parc
        public async Task<Parks> AddParkAsync(Parks park)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5010/api/Parks", park);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Parks>();
        }

        // Méthode pour mettre à jour un parc existant
        public async Task<Parks> UpdateParkAsync(Parks park)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5010/api/Parks/{park.Id}", park);
            response.EnsureSuccessStatusCode();
            return park;
        }

        // Méthode pour supprimer un parc
        public async Task<HttpStatusCode> DeleteParkAsync(int id)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"http://localhost:5010/api/Parks/{id}");
            return response.StatusCode;
        }
    }
}