using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class Ogsm
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Title { get; set; }
    public Ogsm? Parent { get; set; }
    public int? ParentID { get; set; }
    public ICollection<Goal> Goals { get; set; }
    public ICollection<Strategy> Strategies { get; set; }
    public ICollection<Ogsm> Children { get; set; }
}
