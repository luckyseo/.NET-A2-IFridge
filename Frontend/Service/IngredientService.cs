using System.Diagnostics.Contracts;
using Frontend.Models;

namespace Frontend.Service
{
    public class IngredientService
    {
        //Call API to get data from backend 

        private readonly HttpClient _httpClient;
        private readonly LoginService _loginService;
        private ExpiredIngredientInfo _currentExpiredIngredient;

        public IngredientService(HttpClient httpClient, LoginService loginService)
        {
            _httpClient = httpClient;
            _loginService = loginService;

        }

        //get the expiring ingredient return a list
        public async Task<List<Ingredient>> GetExpiring()
        {
            return await _httpClient.GetFromJsonAsync<List<Ingredient>>("http://localhost:5037/api/ingredient/expired");
        }

        //get all ingredients
        public async Task<List<Ingredient>> GetAllIngredients(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<Ingredient>>("http://localhost:5037/api/ingredient/all/" + userId);
        }

        //Edit later for available ingredients

        public async Task<bool> AddIngredient(Ingredient model)
        {
            try
            {
                int userId = _loginService.GetUserId();

                if (userId == -1)
                {
                    throw new InvalidOperationException("User is not logged in.");
                }

                var response = await _httpClient.PostAsJsonAsync("http://localhost:5037/api/ingredient", new
                {
                    Name = model.Name,
                    Quantity = model.Quantity,
                    Category = model.Category,
                    OpenedDate = model.OpenedDate,
                    ExpiredDate = model.ExpiredDate,
                    UserId = userId
                });
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {response.StatusCode}");
                Console.WriteLine($"Error details: {errorContent}");

                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine($"add ingredient Error: {e.Message}");
                return false;
            }
        }
        // private List<Ingredient> _ingredients = new();
        // public void AddIngredient(Ingredient ingredient)
        // {
        //     _ingredients.Add(ingredient);
        // }

        // public void DeleteIngredient(Ingredient ingredient)
        // {
        //     _ingredients.Remove(ingredient);
        // }

        public async Task<bool> UpdateIngredient(Ingredient ingredient)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"http://localhost:5037/api/ingredient/{ingredient.Id}", ingredient);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Update ingredient Error: {e.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteIngredient(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://localhost:5037/api/ingredient/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Delete ingredient Error: {e.Message}");
                return false;
            }
        }


        //expired ingedient dto 
        public class ExpiredIngredientInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime ExpiredDate { get; set; }
        }

    }
}