using System.ComponentModel.DataAnnotations;
namespace nhs_ogsm.Data;

public class Group
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public ICollection<User>? Users { get; set; }
}