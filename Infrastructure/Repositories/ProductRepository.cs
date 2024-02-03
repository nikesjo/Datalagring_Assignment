using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ProductRepository(ProductCatalogContext productCatalogContext) : Repo<Product, ProductCatalogContext>(productCatalogContext), IProductRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
}
