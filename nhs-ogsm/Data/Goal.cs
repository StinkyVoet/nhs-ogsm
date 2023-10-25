using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class Goal
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; } 
    public DateTime EndDate { get; set; }
    public ICollection<Stratagy> Strategies { get; set; }
    
    // Parent OGSM
    public int ParentOgsmID { get; set; }
    public Ogsm Ogsm { get; set; } = null!;
    
    // public ICollection<Group>? AssignedGroups { get; set; }
    // public ICollection<Account>? AssignedAccounts { get; set; }
}