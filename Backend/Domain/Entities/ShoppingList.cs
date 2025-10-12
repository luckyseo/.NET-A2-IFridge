namespace Backend.Domain.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Title { get; set; } = "My Shopping List"; //set by default

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string Items { get; set; } = "";
        //store as string, ask user to inut this in a textarea then format as bullet point in frontend 
    }
}