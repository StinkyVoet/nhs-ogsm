﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class Ogsm
{
    [Key]
    public int ID { get; set; }
    public string Title { get; set; }
    public OgsmItems? Parent { get; set; }
    public int? ParentID { get; set; }
    public ICollection<Goal> Goals { get; set; }
    public ICollection<OgsmItems> Children { get; set; }
}
