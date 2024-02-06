using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class ProductService(CategoryRepository categoryRepository, CurrencyRepository currencyRepository, ManufactureRepository manufactureRepository, ProductPriceRepository productPriceRepository, ProductRepository productRepository) : IProductService
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly CurrencyRepository _currencyRepository = currencyRepository;
    private readonly ManufactureRepository _manufactureRepository = manufactureRepository;
    private readonly ProductPriceRepository _productPriceRepository = productPriceRepository;
    private readonly ProductRepository _productRepository = productRepository;
    public async Task<ProductDto> CreateProductAsync(ProductRegDto productRegDto)
    {
        try
        {
            if (!await _productRepository.ExistsAsync(x => x.ArticleNumber == productRegDto.ArticleNumber))
            {
                var manufactureEntity = await _manufactureRepository.GetAsync(x => x.Manufacture1 == productRegDto.Manufacture);
                if (manufactureEntity == null)
                {
                    manufactureEntity = await _manufactureRepository.CreateAsync(new Manufacture { Manufacture1 = productRegDto.Manufacture });
                }

                var categoryEntity = await _categoryRepository.GetAsync(x => x.CategoryName == productRegDto.CategoryName);
                if (categoryEntity == null)
                {
                    categoryEntity = await _categoryRepository.CreateAsync(new Category { CategoryName = productRegDto.CategoryName });
                }

                var currencyEntity = await _currencyRepository.GetAsync(x => x.Code == productRegDto.CurrencyCode);
                if (currencyEntity == null)
                {
                    currencyEntity = await _currencyRepository.CreateAsync(new Currency
                    {
                        Code = productRegDto.CurrencyCode!,
                        Currency1 = productRegDto.Currency!
                    });
                }

                var productEntity = await _productRepository.CreateAsync(productRegDto);
                return productEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
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
            foreach (var productEntity in productEntities)
            {
                products.Add(productEntity);
            }
            return products;
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
