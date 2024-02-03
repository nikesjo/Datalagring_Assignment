using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

public partial class Product
{
    [Key]
    [StringLength(250)]
    public string ArticleNumber { get; set; } = null!;

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Specification { get; set; }

    public int ManufactureId { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ManufactureId")]
    [InverseProperty("Products")]
    public virtual Manufacture Manufacture { get; set; } = null!;

    [InverseProperty("ArticleNumberNavigation")]
    public virtual ProductPrice? ProductPrice { get; set; }
}
