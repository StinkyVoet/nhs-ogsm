using System.ComponentModel.DataAnnotations;
namespace nhs_ogsm.Data;

public class Group
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Ogsm>? Ogsms { get; set; } = new List<Ogsm>();
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
     
        Group other = (Group)obj;
        return
            ID == other.ID &&
            Name == other.Name;
    }
}