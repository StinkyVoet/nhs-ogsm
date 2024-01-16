using System.ComponentModel.DataAnnotations;
namespace nhs_ogsm.Data;

public class Group
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public int LeaderID { get; set; }
    public User Leader { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Ogsm>? Ogsms { get; set; } = new List<Ogsm>();
}