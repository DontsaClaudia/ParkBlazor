using ParkBlazor.Models;
using System.Net;

namespace ParkBlazor.Services
{
    public class RoomsService
    {
        private readonly HttpClient _httpClient;

        public RoomsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Rooms>> GetAllRoomsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Rooms>>("https://localhost:7227/api/Rooms");
        }

        public async Task<Rooms> GetRoomByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Rooms>($"https://localhost:7227/api/Rooms/{id}");
        }

        public async Task<Rooms> AddRoomAsync(Rooms room)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7227/api/Rooms", room);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rooms>();
        }

        public async Task<Rooms> UpdateRoomAsync(Rooms room)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7227/api/Rooms/{room.Id}", room);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Rooms>();
        }

        public async Task<HttpStatusCode> DeleteRoomAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7227/api/Rooms/{id}");
            return response.StatusCode;
        }

        public async Task<List<Rooms>> SearchRoomsAsync(string name, int? parksId)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(name))
                queryParams.Add($"name={name}");
            if (parksId.HasValue)
                queryParams.Add($"parksId={parksId.Value}");

            var queryString = string.Join("&", queryParams);
            return await _httpClient.GetFromJsonAsync<List<Rooms>>($"https://localhost:7227/api/Rooms/search?{queryString}");
        }
    }
}
