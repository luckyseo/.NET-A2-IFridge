namespace   Backend.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; } //PK
        public string Name { get; set; } = string.Empty;
        public int? Quantity { get; set; } = 1; //default value is 1
        public bool status { get; set; } = false; //default value is false, not purchased

        //FK
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}