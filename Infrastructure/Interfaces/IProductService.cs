using Infrastructure.Dtos;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(ProductRegDto productRegDto);
    Task<bool> DeleteProductAsync(ProductDto productDto);
    Task<ProductDto> GetProductAsync(Expression<Func<Product, bool>> expression);
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> UpdateProductAsync(ProductDto productDto);
}
