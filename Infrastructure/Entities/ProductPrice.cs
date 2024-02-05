using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

public partial class ProductPrice
{
    [Key]
    [StringLength(250)]
    public string ArticleNumber { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    [ForeignKey("ArticleNumber")]
    [InverseProperty("ProductPrice")]
    public virtual Product ArticleNumberNavigation { get; set; } = null!;

    [ForeignKey("CurrencyCode")]
    [InverseProperty("ProductPrices")]
    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;
}
