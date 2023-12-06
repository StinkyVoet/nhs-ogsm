﻿using System.ComponentModel.DataAnnotations;
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
    public ICollection<Ogsm> Children { get; set; }

    [NotMapped]
    public string Url => "dashboard/" + ID;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
     
        Ogsm other = (Ogsm)obj;
        return
            ID == other.ID &&
            Title == other.Title &&
            ParentID == other.ParentID;
    }
}
