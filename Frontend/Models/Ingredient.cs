using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This is Required Field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This is Required Field")]
        public int Category { get; set; }
        public int Quantity { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}