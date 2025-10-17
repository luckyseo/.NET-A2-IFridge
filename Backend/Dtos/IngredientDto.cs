//For expired date notification 
//only need ingredient name and expired date 
//condition for expired ingredient -> 
//red- is expired and orange -> will expired within three days

namespace Backend.Dtos;
    public class IngredientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }


