using Infrastructure.Dtos;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(ProductRegDto productRegDto);
    Task<bool> DeleteProductAsync(ProductDto productDto);
    Task<ProductDto> GetUserAsync(Expression<Func<Product, bool>> expression);
    Task<IEnumerable<ProductDto>> GetUsersAsync();
    Task<ProductDto> UpdateUserAsync(ProductDto productDto);
}
