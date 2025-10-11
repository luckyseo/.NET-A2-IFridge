using System.IO.Pipelines;
using System.ComponentModel.DataAnnotations;
namespace Frontend.Models;

public class RegisterModel
{
    [Required]
    public string id { get; set; }
    [Required]
    public string pw { get; set; }
    [Required]
    public string firstName { get; set; }
    [Required]
    public string lastName { get; set; }
    
}