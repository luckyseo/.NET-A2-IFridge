using System.ComponentModel.DataAnnotations;
namespace Frontend.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This is Required Field")]
        public string Name { get; set; }
        public int? Quantity { get; set; }


    }
}