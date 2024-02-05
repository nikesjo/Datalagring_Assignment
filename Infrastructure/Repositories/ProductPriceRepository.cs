using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductPriceRepository(ProductCatalogContext productCatalogContext) : Repo<ProductPrice, ProductCatalogContext>(productCatalogContext), IProductPriceRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;

    public override async Task<IEnumerable<ProductPrice>> GetAsync()
    {
        try
        {
            var entities = await _productCatalogContext.ProductPrices
                .Include(i => i.CurrencyCodeNavigation)
                .Include(i => i.ArticleNumberNavigation).ThenInclude(i => i.Manufacture)
                .Include(i => i.ArticleNumberNavigation).ThenInclude(i => i.Category)
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch { }

        return null!;
    }

    public override async Task<ProductPrice> GetAsync(Expression<Func<ProductPrice, bool>> expression)
    {
        try
        {
            var entity = await _productCatalogContext.ProductPrices
                .Include(i => i.CurrencyCodeNavigation)
                .Include(i => i.ArticleNumberNavigation).ThenInclude(i => i.Manufacture)
                .Include(i => i.ArticleNumberNavigation).ThenInclude(i => i.Category)
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
