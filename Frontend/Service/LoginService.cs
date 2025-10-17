using Frontend.Models;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Frontend.Service;

public class LoginService
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedSessionStorage _sessionStorage;
    private UserModel _currentUser;
    private bool _isInitialized = false;

    public LoginService(HttpClient httpClient, ProtectedSessionStorage sessionStorage)
    {
        _httpClient = httpClient;
        _sessionStorage = sessionStorage;
    }

    public UserModel? CurrentUser => _currentUser;
    public bool IsAuthenticated => _currentUser != null;

    public async Task InitializeAsync()
    {
        if (_isInitialized)
        {
            Console.WriteLine("LoginService already initialized.");
            return;
        }
        try{
            var result = await _sessionStorage.GetAsync<UserModel>("currentUser");

        if (result.Success)
        {
               _currentUser = result.Success ? result.Value : null;
            Console.WriteLine($"Restored session for user: {_currentUser.loginId}");
        }
        else
        {
            Console.WriteLine("No existing session found.");
        }
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error during initialization: {e.Message}");
        }
       finally
       {
            _isInitialized = true;
       }
    }
    public async Task<bool> LoginAsync(LoginModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("login/auth", new { loginId = model.loginId, pw = model.pw });

            if (response.IsSuccessStatusCode)
            {
                _currentUser = await response.Content.ReadFromJsonAsync<UserModel>();
                await _sessionStorage.SetAsync("currentUser", _currentUser);
                Console.WriteLine($"Login successful for user: {_currentUser.loginId}");
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
                loginId = model.loginId,
                pw = model.pw,
                Allergies = allergyList
            });
            if (response.IsSuccessStatusCode)
            {
                _currentUser = await response.Content.ReadFromJsonAsync<UserModel>();
                await _sessionStorage.SetAsync("currentUser", _currentUser);
                Console.WriteLine($"Registration successful for user: {_currentUser.loginId}");
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

    public async Task<bool> CheckIdExistsAsync(string loginId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"login/check/{loginId}");
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
    public async Task Logout()
    {
        _currentUser = null;
    }

    public int GetUserId()
    {
        return _currentUser?.Id ?? -1;
    }
    
    public UserModel GetUserInfo()
    {
        return _currentUser;
    }
    // //DTO
    // public class UserInfo
    // {
    //     public int Id { get; set; }
    //     public string loginId { get; set; } = string.Empty;
    //     public string firstName { get; set; } = string.Empty;
    //     public string lastName { get; set; } = string.Empty;
    //     public string preferredName { get; set; } = string.Empty;

    //     public string Allergies { get; set; } = string.Empty;
    //     public string pw { get; set; } = string.Empty;
    // }
}