﻿using System.ComponentModel.DataAnnotations;
namespace nhs_ogsm.Data;

public class Group
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public ICollection<Account>? Members { get; set; } = new List<Account>();
    public ICollection<Ogsm>? Ogsms { get; set; } = new List<Ogsm>();
}