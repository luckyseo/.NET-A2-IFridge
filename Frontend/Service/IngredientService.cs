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
        public async Task<List<IngredientDto>> GetExpiring()
        {
            return await _httpClient.GetFromJsonAsync<List<IngredientDto>>("https://localhost:5001/api/ingredients/expired");
        }


        //Edit later for available ingredients


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