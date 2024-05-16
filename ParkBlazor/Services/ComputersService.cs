using ParkBlazor.Models;

namespace ParkBlazor.Services
    
{
    public class ComputersService
    {
        private readonly HttpClient _httpClient= new HttpClient();

        public async Task<List<Computers>> List()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Computers>>("https://localhost:7195/api/Computers");
            return result;
           
        }
    }
}
