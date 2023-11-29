using System.ComponentModel.DataAnnotations;
namespace nhs_ogsm.Data;

public class Account
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public bool isAdmin { get; set; }
    public ICollection<Group> Groups { get; set; } = new List<Group>();
}