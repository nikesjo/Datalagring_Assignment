using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CategoryRepository(ProductCatalogContext productCatalogContext) : Repo<Category, ProductCatalogContext>(productCatalogContext), ICategoryRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;

    public override async Task<IEnumerable<Category>> GetAsync()
    {
        try
        {
            var entities = await _productCatalogContext.Categories
                .Include(i => i.Products).ThenInclude(i => i.Manufacture)
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

    public override async Task<Category> GetAsync(Expression<Func<Category, bool>> expression)
    {
        try
        {
            var entity = await _productCatalogContext.Categories
                .Include(i => i.Products).ThenInclude(i => i.Manufacture)
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
