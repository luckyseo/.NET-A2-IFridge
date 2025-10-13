using System.IO.Pipelines;
using System.ComponentModel.DataAnnotations;
namespace Frontend.Models;

public class RegisterModel
{
    [Required(ErrorMessage ="This is Required Field")]
    public string loginId { get; set; }
    [Required(ErrorMessage ="This is Required Field")]
    public string pw { get; set; }
    [Required(ErrorMessage ="This is Required Field")]
    public string firstName { get; set; }
    [Required(ErrorMessage ="This is Required Field")]
    public string lastName { get; set; }

    public string preferredName { get; set; }

    public string allergies { get; set; }
    
}