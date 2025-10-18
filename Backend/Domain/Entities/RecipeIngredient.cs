
namespace Backend.Domain.Entities
{
    public class RecipeIngredient
    {
        //fk to recipe 
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        //fk to ingredient 
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        // public string IngredientName { get; set; } = string.Empty;

        public int Quantity { get; set; } //required amount for a recipe

    }

    //this will be used for make recipe suggestions based on available ingredient
    // match by name ingredient name == recipeIngredient name
}

