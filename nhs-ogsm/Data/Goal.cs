﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nhs_ogsm.Data;

public class Goal
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } 
    public string? Description { get; set; }
    public bool IsDone { get; set; } = false;
    [Required]
    public DateTimeOffset EndDate { get; set; }
    // Child Strategy
    public ICollection<Strategy>? Strategies { get; set; }
    
    // Parent OGSM
    public int ParentOgsmID { get; set; }
    public Ogsm Ogsm { get; set; } = null!;
    
    // public ICollection<Group>? AssignedGroups { get; set; }
    // public ICollection<Account>? AssignedAccounts { get; set; }
}