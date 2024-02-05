using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CurrencyRepository(ProductCatalogContext productCatalogContext) : Repo<Currency, ProductCatalogContext>(productCatalogContext), ICurrencyRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;

    public override async Task<IEnumerable<Currency>> GetAsync()
    {
        try
        {
            var entities = await _productCatalogContext.Currencies
                .Include(i => i.ProductPrices).ThenInclude(i => i.ArticleNumberNavigation).ThenInclude(i => i.Manufacture)
                .Include(i => i.ProductPrices).ThenInclude(i => i.ArticleNumberNavigation).ThenInclude(i => i.Category)
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public override async Task<Currency> GetAsync(Expression<Func<Currency, bool>> expression)
    {
        try
        {
            var entity = await _productCatalogContext.Currencies
                .Include(i => i.ProductPrices).ThenInclude(i => i.ArticleNumberNavigation).ThenInclude(i => i.Manufacture)
                .Include(i => i.ProductPrices).ThenInclude(i => i.ArticleNumberNavigation).ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }
}
