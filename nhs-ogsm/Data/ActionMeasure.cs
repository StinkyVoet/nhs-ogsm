using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class ActionMeasure
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } 
    public bool IsDone { get; set; } = false;
    [Required]
    // Parent Strategy 
    public int ParentStrategyID { get; set; }
    public Strategy Strategy;
    // parent Users
    public ICollection<User> Users { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
     
        ActionMeasure other = (ActionMeasure)obj;
        return
            ID == other.ID &&
            Name == other.Name &&
            IsDone == other.IsDone;
    }
}