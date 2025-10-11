using System.Dynamic;

namespace Backend.Domain.Entities;
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime OpenedDate { get; set; }
    public DateTime ExpiredDate { get; set; }


}


//use data migration to notify expired date 

//Will do this later: apply migration
//dotnet ef migrations add AddIngredientTable
//dotnet ef database update


