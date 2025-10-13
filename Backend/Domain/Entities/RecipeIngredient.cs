
namespace Backend.Domain.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; set; } //PK

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string IngredientName { get; set; } = string.Empty;
        //recipe may have ingredients not in fridge

        public int RequiredQuantity { get; set; }

    }
}

