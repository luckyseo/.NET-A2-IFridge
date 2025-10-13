using System.IO.Pipelines;
using System.ComponentModel.DataAnnotations;
namespace Frontend.Models;

public class LoginModel
{
    [Required]
    public string loginId { get; set; }
    [Required]
    public string pw{ get; set; }
}