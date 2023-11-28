using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class ActionMeasure
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    // Parent Strategy 
    public int ParentStrategyID { get; set; }
    public Strategy Strategy;
    // parent Users
    public ICollection<User> Users { get; set; }
}