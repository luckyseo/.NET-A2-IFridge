
namespace Backend.Domain.Entities
{
    public class RecipeIngredient
    {
        //fk to recipe 
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public Recipe Recipe { get; set; } = null!;
        //fk to ingredient 
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public int Quantity { get; set; } //required amount for a recipe

    }
}