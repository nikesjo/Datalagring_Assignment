using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ManufactureRepository(ProductCatalogContext productCatalogContext) : Repo<Manufacture, ProductCatalogContext>(productCatalogContext), IManufactureRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
}
