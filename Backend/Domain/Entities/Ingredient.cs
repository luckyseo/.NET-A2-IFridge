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

    //for many-many relationship with RecipeIngredient
    public List<RecipeIngredient> RecipeIngredients { get; set; } = new();

}

public enum IngredientCategory
{
    Dairy = 0,
    Meat = 1,
    VegetableAndFruit = 2,
    Grain = 3 ,
    Beverage = 4,
    Other =5
}




