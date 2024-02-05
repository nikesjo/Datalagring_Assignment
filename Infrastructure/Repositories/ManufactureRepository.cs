using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ManufactureRepository(ProductCatalogContext productCatalogContext) : Repo<Manufacture, ProductCatalogContext>(productCatalogContext), IManufactureRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;

    public override async Task<IEnumerable<Manufacture>> GetAsync()
    {
        try
        {
            var entities = await _productCatalogContext.Manufactures
                .Include(i => i.Products).ThenInclude(i => i.Category)
                .Include(i => i.Products).ThenInclude(i => i.ProductPrice).ThenInclude(i => i.CurrencyCodeNavigation)
                .ToListAsync();
            if (entities.Count != 0)
            {
                return entities;
            }
        }
        catch { }

        return null!;
    }

    public override async Task<Manufacture> GetAsync(Expression<Func<Manufacture, bool>> expression)
    {
        try
        {
            var entity = await _productCatalogContext.Manufactures
                .Include(i => i.Products).ThenInclude(i => i.Category)
                .Include(i => i.Products).ThenInclude(i => i.ProductPrice).ThenInclude(i => i.CurrencyCodeNavigation)
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
