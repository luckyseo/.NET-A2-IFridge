using Frontend.Models;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

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
            var response = await _httpClient.PostAsJsonAsync("login/auth", new { id = model.id, pw = model.pw });
            
            if (response.IsSuccessStatusCode)
            {
                _currentUser = await response.Content.ReadFromJsonAsync<UserInfo>();
                return true;
            }
            return false;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Login Error: {e.Message}");
            return false;
        }
    }

    public async Task<bool> RegisterAsync(RegisterModel model)
    {
        try
        {
            List<string>? allergyList = null;
            if (!string.IsNullOrWhiteSpace(model.allergies))
            {
                allergyList = model.allergies.Split(','
                ).Select(Item => Item.Trim()).Where(Item => !string.IsNullOrWhiteSpace(Item)).ToList();
            }

            var response = await _httpClient.PostAsJsonAsync("login/register", new
            {
                firstName = model.firstName,
                lastName = model.lastName,
                preferredName = model.preferredName,
                id = model.id,
                pw = model.pw,
                Allergies = allergyList
            });
            if (response.IsSuccessStatusCode)
            {
                _currentUser = await response.Content.ReadFromJsonAsync<UserInfo>();
                return true;
            }

            return false;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Login Error: {e.Message}");
            return false;
        }
    }

    public async Task<bool> CheckIdExistsAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"login/check/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CheckIdResponse>();
                return result?.exists ?? false;
            }
            return false;
        }
        catch(Exception e)
        {
            Console.WriteLine($"CheckId Error: {e.Message}");
            return false;
        }
    }
    public class CheckIdResponse
    {
        public bool exists{ get; set; }
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