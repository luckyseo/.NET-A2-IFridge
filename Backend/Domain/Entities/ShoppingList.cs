using System.Collections;

namespace Backend.Domain.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; } //PK
        public string Title { get; set; } = "My Shopping List"; //set by default

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ArrayList<Item> Items { get; set; } = new();
        //store as string, ask user to inut this in a textarea then format as bullet point in frontend 
    
        //FK
        public int UserId { get; set; }
        public User User { get; set; }
    }
}