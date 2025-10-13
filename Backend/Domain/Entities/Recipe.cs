using System.Runtime.InteropServices;

namespace Backend.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; } = string.Empty; //optional

        //associative entity between ingredient and recipe - call the obj
        public List<RecipeIngredient> Ingredients { get; set; } = new();

        public string Steps { get; set; } = string.Empty;


    }
}