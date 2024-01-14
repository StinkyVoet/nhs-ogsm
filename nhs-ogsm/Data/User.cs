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
    [Required]
    public string FirstName { get; set; } 
    [Required]
    public string LastName { get; set; }
    public bool IsAdmin { get; set; } = false;
    // parent groups
    public ICollection<Group>? Groups { get; set; }
    // child Actions
    public ICollection<ActionMeasure>? Actions { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
     
        User other = (User)obj;
        return
            ID == other.ID &&
            Email == other.Email &&
            FirstName == other.FirstName &&
            LastName == other.LastName &&
            IsAdmin == other.IsAdmin;
    }
}