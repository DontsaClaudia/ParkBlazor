using Blazored.LocalStorage;
using ParkBlazor.Models;
using System.Net;

public class ComputersService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private string _token;

    public ComputersService(HttpClient httpClient, ILocalStorageService localStorage)
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

    public async Task<List<Computers>> GetAllComputersAsync()
    {
        await InitializeAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<List<Computers>>("http://localhost:5010/api/Computers");
    }

    public async Task<Computers> GetComputerByIdAsync(int id)
    {
        await InitializeAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<Computers>($"http://localhost:5010/api/Computers/{id}");
    }

    public async Task<Computers> AddComputerAsync(Computers computer)
    {
        await InitializeAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5010/api/Computers", computer);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Computers>();
        }
        else
        {
            return new Computers();
        }
    }

    public async Task<Computers> UpdateComputerAsync(Computers computer)
    {
        await InitializeAuthorizationHeader();
        var response = await _httpClient.PutAsJsonAsync($"http://localhost:5010/api/Computers/{computer.Id}", computer);
        response.EnsureSuccessStatusCode();
        return computer;
    }

    public async Task<HttpStatusCode> DeleteComputerAsync(int id)
    {
        await InitializeAuthorizationHeader();
        var response = await _httpClient.DeleteAsync($"http://localhost:5010/api/Computers/{id}");
        return response.StatusCode;
    }

    public async Task<List<Computers>> SearchComputersAsync(string manufacturer, string model, int? roomId, int? minMemory, int? maxMemory)
    {
        await InitializeAuthorizationHeader();
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
        return await _httpClient.GetFromJsonAsync<List<Computers>>($"http://localhost:5010/api/Computers/search?{queryString}");
    }

    public async Task<bool> ComputerExistsAsync(Computers computer)
    {
        await InitializeAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5010/api/Computers/exists", computer);
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}