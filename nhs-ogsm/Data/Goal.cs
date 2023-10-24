using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class Goal
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; } 
    public List<string>? Stratagies { get; set; }
    public int OgsmItemID { get; set; }
    public OgsmItems OgsmItem { get; set; } = null!;
    
    // public ICollection<Group>? AssignedGroups { get; set; }
    // public ICollection<Account>? AssignedAccounts { get; set; }
}