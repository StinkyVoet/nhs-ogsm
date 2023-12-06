using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace nhs_ogsm.Data;
public class User
{
    [Key]
    public int ID { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public bool IsAdmin { get; set; } = false;
    // parent groups
    public ICollection<Group>? Groups { get; set; }
    // child Actions
    public ICollection<ActionMeasure>? Actions { get; set; }
}