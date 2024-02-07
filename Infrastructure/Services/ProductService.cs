using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class ProductService(ICategoryRepository categoryRepository, ICurrencyRepository currencyRepository, IManufactureRepository manufactureRepository, IProductPriceRepository productPriceRepository, IProductRepository productRepository) : IProductService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;
    private readonly IManufactureRepository _manufactureRepository = manufactureRepository;
    private readonly IProductPriceRepository _productPriceRepository = productPriceRepository;
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<bool> CreateProductAsync(ProductRegDto productRegDto)
    {
        try
        {
            if (!await _productRepository.ExistsAsync(x => x.ArticleNumber == productRegDto.ArticleNumber))
            {
                var manufactureEntity = await _manufactureRepository.GetAsync(x => x.Manufacture1 == productRegDto.Manufacture);
                manufactureEntity ??= await _manufactureRepository.CreateAsync(new Manufacture { Manufacture1 = productRegDto.Manufacture });

                var categoryEntity = await _categoryRepository.GetAsync(x => x.CategoryName == productRegDto.CategoryName);
                categoryEntity ??= await _categoryRepository.CreateAsync(new Category { CategoryName = productRegDto.CategoryName });

                var currencyEntity = await _currencyRepository.GetAsync(x => x.Code == productRegDto.CurrencyCode);
                currencyEntity ??= await _currencyRepository.CreateAsync(new Currency{ Code = productRegDto.CurrencyCode!, Currency1 = productRegDto.Currency!});
                
                var productEntity = new Product
                {
                    ArticleNumber = productRegDto.ArticleNumber,
                    Title = productRegDto.Title,
                    Description = productRegDto.Description,
                    Specification = productRegDto.Specification,
                    Manufacture = manufactureEntity,
                    Category = categoryEntity,
                    ManufactureId = manufactureEntity.Id,
                    CategoryId = categoryEntity.Id,
                    ProductPrice = new ProductPrice
                    {
                        Price = (decimal)productRegDto.Price!,
                        CurrencyCodeNavigation = currencyEntity,
                    },
                };

                var result = await _productRepository.CreateAsync(productEntity);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return false;
    }

    public async Task<bool> DeleteProductAsync(ProductDto productDto)
    {
        try
        {
            var productEntity = await _productRepository.GetAsync(x => x.ArticleNumber == productDto.ArticleNumber);
            if (productEntity != null)
            {
                await _productPriceRepository.DeleteAsync(x => x.ArticleNumber == productEntity.ArticleNumber);
                await _productRepository.DeleteAsync(x => x.ArticleNumber == productEntity.ArticleNumber);

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return false;
    }

    public async Task<ProductDto> GetProductAsync(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var productEntity = await _productRepository.GetAsync(expression);
            if (productEntity != null)
            {
                return productEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = new List<ProductDto>();
        try
        {
            var productEntities = await _productRepository.GetAsync();
            if (productEntities != null)
            {
                foreach (var productEntity in productEntities)
                {
                    products.Add(productEntity);
                }
                return products;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
    {
        try
        {
            var updateProduct = await _productRepository.GetAsync(x => x.ArticleNumber == productDto.ArticleNumber);
            if (updateProduct != null)
            {
                updateProduct.Title = productDto.Title;
                updateProduct.Description = productDto.Description;
                updateProduct.Specification = productDto.Specification;

                if (productDto.CategoryName != null)
                {
                    var categoryExists = await _categoryRepository.ExistsAsync(x => x.CategoryName == productDto.CategoryName);

                    if (categoryExists)
                    {
                        var existingCategory = await _categoryRepository.GetAsync(c => c.CategoryName == productDto.CategoryName);
                        updateProduct.Category = existingCategory;
                    }
                    else
                    {
                        updateProduct.Category = new Category { CategoryName = productDto.CategoryName };
                    }
                }

                if (productDto.Manufacture != null)
                {
                    var manufactureExists = await _manufactureRepository.ExistsAsync(x => x.Manufacture1 == productDto.Manufacture);

                    if (manufactureExists)
                    {
                        var existingManufacture = await _manufactureRepository.GetAsync(c => c.Manufacture1 == productDto.Manufacture);
                        updateProduct.Manufacture = existingManufacture;
                    }
                    else
                    {
                        updateProduct.Manufacture = new Manufacture { Manufacture1 = productDto.Manufacture };
                    }
                }


                if (updateProduct.ProductPrice != null)
                {
                    updateProduct.ProductPrice.Price = (decimal)productDto.Price!;

                    if (productDto.CurrencyCode != null)
                    {
                        var currencyExists = await _currencyRepository.ExistsAsync(c => c.Code == productDto.CurrencyCode);

                        if (currencyExists)
                        {
                            var existingCurrency = await _currencyRepository.GetAsync(c => c.Code == productDto.CurrencyCode);
                            updateProduct.ProductPrice.CurrencyCodeNavigation = existingCurrency;
                        }
                        else
                        {
                            updateProduct.ProductPrice.CurrencyCodeNavigation = new Currency
                            {
                                Code = productDto.CurrencyCode,
                                Currency1 = productDto.Currency!
                            };
                        }
                    }
                }

                var updatedCustomerEntity = await _productRepository.UpdateAsync(x => x.ArticleNumber == updateProduct.ArticleNumber, updateProduct);

                return productDto;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
