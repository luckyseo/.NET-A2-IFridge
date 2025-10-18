using System.Text.Json.Serialization;

namespace   Backend.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; } //PK
        public string Name { get; set; } = string.Empty;
        public int? Quantity { get; set; } = 1; //default value is 1

      
        public int UserId { get; set; }
        public User User { get; set; }
    }
}