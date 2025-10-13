using System.Dynamic;

namespace Backend.Domain.Entities;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IngredientCategory Category { get; set; }
    public int? Quantity { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

}

public enum IngredientCategory
{
    Diary,
    Meat,
    VegetableAndFruit,
    Grain,
    Beverage,
    Other
}


//use dto to notify expired date 

//Will do this later: apply migration
//dotnet ef migrations add AddIngredientTable
//dotnet ef database update


