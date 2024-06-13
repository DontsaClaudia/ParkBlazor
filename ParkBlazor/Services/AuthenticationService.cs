using Blazored.LocalStorage;

public class AuthenticationService
{
    private readonly ApiService _apiService;
    private readonly ILocalStorageService _localStorage;
    public static string UserToken = string.Empty;

    public AuthenticationService(ApiService apiService, ILocalStorageService localStorage)
    {
        _apiService = apiService;
        _localStorage = localStorage;
    }

    public async Task<bool> Login(string email, string password)
    {
        var loginModel = new { Email = email, Password = password };
        var response = await _apiService.PostAsync<LoginResponse>("login", loginModel);

        if (response != null && !string.IsNullOrEmpty(response.accessToken))
        {
            await SetTokenAsync(response.accessToken);
            return true;
        }

        return false;
    }

    public async Task Logout()
    {
        UserToken = string.Empty;
        _apiService.RemoveAuthorizationHeader();
        await _localStorage.RemoveItemAsync("authToken");
    }

    public async Task SetTokenAsync(string token)
    {
        UserToken = token;
        _apiService.SetAuthorizationHeader(token);
        await _localStorage.SetItemAsync("authToken", token);
    }

    public async Task InitializeAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            await SetTokenAsync(token);
        }
    }
}

public class LoginResponse
{
    public string accessToken { get; set; }
}