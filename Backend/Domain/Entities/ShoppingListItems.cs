namespace Backend.Domain.Entities
{
    public class ShoppingListItems
    {
        //child of shopping lost 
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool IsPurchased { get; set; } = false;

        //to parent 
        public int ShoppingListId { get; set; }
        public ShoppingListItems ShoppingList { get; set; }

    }
}

//Note about storing data from front end 
// Collect shoppinglist data
// Collect all shopping list items the user input
// Create a shopping lost obejcy
// Attach the list of shoppinngList objects to the Items property
// Save ShoppingList entity