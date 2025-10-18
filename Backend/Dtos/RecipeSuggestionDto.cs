using Backend.Domain.Entities; 

namespace Backend.Dtos;

public class RecipeSuggestionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public RecipeCategory Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string Steps { get; set; } = string.Empty;

    public int TotalIngredients { get; set; }
    public int MissingIngredientCounts { get; set; }
    public List<string> MissingIngredients { get; set; } = new();
}

