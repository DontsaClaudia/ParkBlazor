using ParkBlazor.Models;
using System.Net;
using Blazored.LocalStorage;

namespace ParkBlazor.Services
{
    public class RoomsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private string _token;

        public RoomsService(HttpClient httpClient, ILocalStorageService localStorage)
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
        public async Task<List<Rooms>> GetAllRoomsAsync()
        {
            await InitializeAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<Rooms>>("http://localhost:5165/api/Rooms");
        }

        public async Task<Rooms> GetRoomByIdAsync(int id)
        {
            await InitializeAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<Rooms>($"http://localhost:5165/api/Rooms/{id}");
        }

        public async Task<Rooms> AddRoomAsync(Rooms room)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5165/api/Rooms", room);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rooms>();
        }

        public async Task<Rooms> UpdateRoomAsync(Rooms room)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5165/api/Rooms/{room.Id}", room);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rooms>();
        }

        public async Task<HttpStatusCode> DeleteRoomAsync(int id)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"http://localhost:5165/api/Rooms/{id}");
            return response.StatusCode;
        }

        public async Task<List<Rooms>> SearchRoomsAsync(string name, int? parksId)
        {
            await InitializeAuthorizationHeader();
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(name))
                queryParams.Add($"name={name}");
            if (parksId.HasValue)
                queryParams.Add($"parksId={parksId.Value}");

            var queryString = string.Join("&", queryParams);
            return await _httpClient.GetFromJsonAsync<List<Rooms>>($"http://localhost:5165/api/Rooms/search?{queryString}");
        }
    }
}
