namespace Infrastructure.Dtos;

public class ProductRegDto
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Specification { get; set; }
    public string Manufacture { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public decimal? Price { get; set; }
    public string? CurrencyCode { get; set; }
}
