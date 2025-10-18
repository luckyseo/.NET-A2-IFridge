using Backend.Domain.Entities;
namespace Backend.Dtos;

public class ItemUpdateDto
{
    public string Name { get; set; }
    public int? Quantity { get; set; }
}