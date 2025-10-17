using Backend.Domain.Entities; 
namespace Backend.Dtos;

public class IngredientCreatedDto
{
    public string Name { get; set; }
    public IngredientCategory Category { get; set; }
    public int Quantity { get; set; }
    public DateTime OpenedDate { get; set; }
    public DateTime ExpiredDate { get; set; }
    public int UserId { get; set; }
}
