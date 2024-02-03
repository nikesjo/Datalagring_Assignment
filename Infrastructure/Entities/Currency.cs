﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

[Index("Currency1", Name = "UQ__Currenci__AFC4E28416CFE044", IsUnique = true)]
public partial class Currency
{
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("Currency")]
    [StringLength(20)]
    public string Currency1 { get; set; } = null!;

    [InverseProperty("CurrencyCodeNavigation")]
    public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
}
