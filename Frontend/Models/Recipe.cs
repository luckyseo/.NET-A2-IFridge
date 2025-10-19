
namespace Frontend.Models
{
  public class Recipe
  {

    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public RecipeCategory Category { get; set; }

  }

  public enum RecipeCategory
  {
        Salad = 0,
        Soup = 1,
        Main =2,
        Side =3,
        Vegetarian =4,
        Dessert =5
  }
    
 
}