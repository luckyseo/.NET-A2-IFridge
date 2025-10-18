
namespace Backend.Domain.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public int Quantity { get; set; }

        //fk to recipe 
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }

    //this will be used for make recipe suggestions based on available ingredient
    // match by name ingredient name == recipeIngredient name
}

