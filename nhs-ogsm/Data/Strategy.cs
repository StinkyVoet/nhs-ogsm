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
    
    // Parent Goals
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    // Child Action
    public ICollection<ActionMeasure> Actions { get; set; } = new List<ActionMeasure>();
    // Parent OGSM
    public int ParentOgsmID { get; set; }
    public Ogsm Ogsm { get; set; } = null!;
}