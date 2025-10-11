using Frontend.Models;
using System.Net.Http.Json;

namespace Frontend.Service;

public class LoginService
{
    private readonly HttpClient _httpClient;
    private UserInfo _currentUser;

    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public UserInfo? CurrentUser => _currentUser;
    public bool IsAuthenticated => _currentUser != null;

    public async Task<bool> LoginAsync(LoginModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("login/auth", model);
            if (response.IsSuccessStatusCode)
            {
                _currentUser = await response.Content.ReadFromJsonAsync<UserInfo>();
                return true;
            }
            return false;

        }
        catch
        {
            return false;
        }
    }
    public void Logout()
    {
        _currentUser = null;
    }

    //DTO
    public class UserInfo
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string preferredName{ get; set; }
    }
}