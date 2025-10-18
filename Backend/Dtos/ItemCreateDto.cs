using Backend.Domain.Entities; 
namespace Backend.Dtos;

public class ItemCreateDto
{
    public int Id { get; set; } //PK
    public string Name { get; set; }
    public int? Quantity { get; set; }


    public int UserId { get; set; }
}