//service handles api request calling

namespace Frontend.Service
{
    public class IngredientService
    {
        private List<Ingredient> _ingredients = new();
        public void AddIngredient(Ingredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            _ingredients.Remove(ingredient);
        }

        //add edit ingredients

    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}