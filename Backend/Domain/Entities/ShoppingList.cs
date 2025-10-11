namespace Backend.Domain.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Title { get; set; } = "My Shopping List"; //set by default

        public DateTime DateCreated { get; set; } = DateTime.Now;

        //get user input items 
        public List<ShoppingListItems> Items { get; set; } = new(); 

    }
}