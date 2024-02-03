using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CurrencyRepository(ProductCatalogContext productCatalogContext) : Repo<Currency, ProductCatalogContext>(productCatalogContext), ICurrencyRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
}
