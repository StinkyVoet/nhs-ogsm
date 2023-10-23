﻿using System.ComponentModel.DataAnnotations;
namespace nhs_ogsm.Data;

public class Goal
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public List<string>? Stratagies { get; set; }
    public ICollection<Group>? AssignedGroups { get; set; }
    public ICollection<Account>? AssignedAccounts { get; set; }
}