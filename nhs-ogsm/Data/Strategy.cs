using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class Strategy
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
    public bool IsDone { get; set; } = false;
    
    // Parent Goal
    public int ParentGoalID { get; set; }
    public Goal ParentGoal { get; set; } = null!;
}