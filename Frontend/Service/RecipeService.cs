using System.Diagnostics.Contracts;
using Frontend.Models;

namespace Frontend.Service
{
    public class RecipeService
    {
        //Call API to get data from backend 

        private readonly HttpClient _httpClient;
        private readonly LoginService _loginService;

        public RecipeService(HttpClient httpClient, LoginService loginService)
        {
            _httpClient = httpClient;
            _loginService = loginService;

        }
        //get all recipes
        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _httpClient.GetFromJsonAsync<List<Recipe>>("http://localhost:5037/api/recipe");
        }

        //get suggested recipes based on available ingredients
        public async Task<List<RecipeSuggestionModel>> GetSuggestedRecipes(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<RecipeSuggestionModel>>($"http://localhost:5037/api/recipe/user/{userId}/suggestion");
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Recipe>($"http://localhost:5037/api/recipe/{id}");
        }

        public async Task<List<Recipe>>GetRecipeByCategory(string category)
        {
            return await _httpClient.GetFromJsonAsync<List<Recipe>>($"http://localhost:5037/api/recipe/category/{category}");
        }
 
   

    }
}