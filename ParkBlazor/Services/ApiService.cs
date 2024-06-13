using System.Net.Http.Headers;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> PostAsync<T>(string uri, object data)
    {
        var response = await _httpClient.PostAsJsonAsync(uri, data);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            
            throw new HttpRequestException(response.ReasonPhrase);
        }
    }

    public void SetAuthorizationHeader(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    public void RemoveAuthorizationHeader()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}