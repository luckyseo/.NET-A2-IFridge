
namespace Frontend.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string loginId { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string preferredName { get; set; } = string.Empty;

        public string Allergies { get; set; } = string.Empty;
        public string pw { get; set; } = string.Empty;
    }
}