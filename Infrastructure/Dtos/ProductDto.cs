using Infrastructure.Entities;
using System.Runtime.CompilerServices;

namespace Infrastructure.Dtos;

public class ProductDto
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Specification { get; set; }
    public string Manufacture { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public decimal? Price { get; set; }
    public string? CurrencyCode { get; set; }

    public static implicit operator ProductDto(Product product)
    {
        return new ProductDto
        {
            ArticleNumber = product.ArticleNumber,
            Title = product.Title,
            Description = product.Description,
            Specification = product.Specification,
            Manufacture = product.Manufacture.Manufacture1,
            CategoryName = product.Category.CategoryName,
            Price = product.ProductPrice!.Price,
            CurrencyCode = product.ProductPrice.CurrencyCodeNavigation.Code
        };
    }
}
