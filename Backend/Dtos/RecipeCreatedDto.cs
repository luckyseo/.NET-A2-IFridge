using Backend.Domain.Entities;
namespace Backend.Dtos;

public class RecipeCreatedDto
{
    public string Name { get; set; } = string.Empty;
    public RecipeCategory Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;  
    public string Steps { get; set; } = string.Empty;
}
