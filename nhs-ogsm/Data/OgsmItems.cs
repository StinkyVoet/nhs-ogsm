using System.ComponentModel.DataAnnotations;

namespace nhs_ogsm.Data;

public class OgsmItems
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; }
}
