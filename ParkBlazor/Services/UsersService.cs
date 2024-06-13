using Blazored.LocalStorage;
using ParkBlazor.Models;
using System.Net;
using System.Net.Http.Headers;


namespace ParkBlazor.Services
{
    public class UsersService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private string _token;

        public UsersService(HttpClient httpClient, ILocalStorageService localStorage)
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
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
        }

        public async Task SetAuthorizationHeader(string accessToken)
        {
            _token = accessToken;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            await _localStorage.SetItemAsync("authToken", accessToken);
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.GetAsync("http://localhost:5165/api/Users");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Users>>();
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            await InitializeAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<Users>($"http://localhost:5165/api/Users/{id}");
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5165/api/Users", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Users>();
        }

        public async Task<Users> UpdateUserAsync(Users user)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5165/api/Users/{user.Id}", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Users>();
        }

        public async Task<HttpStatusCode> DeleteUserAsync(int id)
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"http://localhost:5165/api/Users/{id}");
            return response.StatusCode;
        }

        public async Task<List<Rules>> GetRolesAsync()
        {
            await InitializeAuthorizationHeader();
            var response = await _httpClient.GetAsync("http://localhost:5165/api/Rules");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Rules>>();
        }
    }
}