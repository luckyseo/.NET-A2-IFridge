
namespace Frontend.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}