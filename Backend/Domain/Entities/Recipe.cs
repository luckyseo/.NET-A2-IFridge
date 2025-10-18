using System.Runtime.InteropServices;

namespace Backend.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;  //nullable
        public string Steps { get; set; } = string.Empty;

        //Each recipe has a list of ingedients needed -> for matching with available ingredient
        public List<RecipeIngredient> Ingredients { get; set; } = new();

        //filter by categry 
        public RecipeCategory Category { get; set; }
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