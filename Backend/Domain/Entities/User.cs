using System.Dynamic;

namespace Backend.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string preferredName { get; set; } = string.Empty;
    public string id{ get; set; } = string.Empty;
    public int pw { get; set; }

    public List<String> Allergies { get; set; } = new List<String>();
}
