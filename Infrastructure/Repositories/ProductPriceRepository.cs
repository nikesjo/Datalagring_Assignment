using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ProductPriceRepository(ProductCatalogContext productCatalogContext) : Repo<ProductPrice, ProductCatalogContext>(productCatalogContext), IProductPriceRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
}
