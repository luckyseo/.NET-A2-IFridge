using System.Runtime.InteropServices;

namespace Backend.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RecipeCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;  //nullable
        public string Steps { get; set; } = string.Empty;

        public List<string> IngredientList { get; set; } = new(); //required ingredient (name) need for each
        public List<RecipeIngredient> RecipeIngredients { get; set; } = new();
    }

    public enum RecipeCategory
    {
        Salad,
        Soup,
        Main,
        Side,
        Vegetarian,
        Dessert
    }
}