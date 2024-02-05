using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductRepository(ProductCatalogContext productCatalogContext) : Repo<Product, ProductCatalogContext>(productCatalogContext), IProductRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;

    public override async Task<IEnumerable<Product>> GetAsync()
    {
        try
        {
            var entities = await _productCatalogContext.Products
                .Include(i => i.ProductPrice).ThenInclude(i => i.CurrencyCodeNavigation)
                .Include(i => i.Manufacture)
                .Include(i => i.Category)
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch { }

        return null!;
    }

    public override async Task<Product> GetAsync(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var entity = await _productCatalogContext.Products
                .Include(i => i.ProductPrice).ThenInclude(i => i.CurrencyCodeNavigation)
                .Include(i => i.Manufacture)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }
        }
        catch { }

        return null!;
    }
}
