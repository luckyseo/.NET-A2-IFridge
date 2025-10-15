using System.Diagnostics.Contracts;
using Frontend.Models;

namespace Frontend.Service
{
    public class IngredientService
    {
        //Call API to get data from backend 

        private readonly HttpClient _httpClient;
        private ExpiredIngredientInfo _currentExpiredIngredient;

        public IngredientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //get the expiring ingredient return a list
        public async Task<List<Ingredient>> GetExpiring()
        {
            return await _httpClient.GetFromJsonAsync<List<Ingredient>>("https://localhost:5001/api/ingredients/expired");
        }

        //get all ingredients
        public async Task<List<Ingredient>> GetAllIngredients()
        {
            return await _httpClient.GetFromJsonAsync<List<Ingredient>>("https://localhost:5001/api/ingredients");
        }

        //Edit later for available ingredients

        public async Task<bool> AddIngredient(Ingredient model)
        {
        try
        {
        
            var response = await _httpClient.PostAsJsonAsync("api/ingredient", new
            {
                Name = model.Name,
                Quantity = model.Quantity,
                Category = model.Category,
                OpenedDate = model.OpenedDate,
                ExpiredDate = model.ExpiredDate
            });
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

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


    }


    //expired ingedient dto 
    public class ExpiredIngredientInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

}