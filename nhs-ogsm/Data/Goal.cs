using System.ComponentModel.DataAnnotations;
namespace nhs_ogsm.Data;

public class Goal
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public Dictionary<int, string>? Stratagies { get; set; }
    public List<Group>? AssignedGroups { get; set; }
    public List<Account>? AssignedAccounts { get; set; }
}