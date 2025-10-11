namespace Frontend.Models
{   
    public class ShoppingListModel
    {
        public string Title { get; set; } = "";
        public List<string> Items { get; set; } = new();
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}