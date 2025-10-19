namespace Frontend.Models;

    public class RecipeSuggestionModel: Recipe {
        public int Id { get; set; }
        public string Steps { get; set; } = string.Empty;

        public int TotalIngredients { get; set; }
        public int MissingIngredientCounts { get; set; }
        public List<string> MissingIngredients { get; set; } = new();
    }
 