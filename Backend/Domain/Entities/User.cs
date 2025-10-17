using System.Dynamic;

namespace Backend.Domain.Entities;
public class User
{
    public int Id { get; set; } //PK - autoincrement form db
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string preferredName { get; set; } = string.Empty;
    public string loginId{ get; set; } = string.Empty; //User creates
    public string pw { get; set; }

    public List<String> Allergies { get; set; } = new List<String>();

    //a user can have many ingredient
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}


//References
//