using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

[Index("Manufacture1", Name = "UQ__Manufact__C624340F6F22F980", IsUnique = true)]
public partial class Manufacture
{
    [Key]
    public int Id { get; set; }

    [Column("Manufacture")]
    [StringLength(50)]
    public string Manufacture1 { get; set; } = null!;

    [InverseProperty("Manufacture")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
