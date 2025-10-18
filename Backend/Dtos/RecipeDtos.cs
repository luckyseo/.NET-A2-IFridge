using Backend.Domain.Entities; 
namespace Backend.Dtos;

public class RecipeDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public  RecipeCategory Category { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<RecipeIngredient> ingredients { get; set; }
}
