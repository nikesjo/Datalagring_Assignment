using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class ProductService_Tests
{
    private readonly ProductCatalogContext _productCatalogContext =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateProductAsync_Should_CreateANewProduct_And_ReturnTrue()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufactureRepository, productPriceRepository, productRepository);
        var productRegDto = new ProductRegDto
        {
            ArticleNumber = "1",
            Title = "Test",
            Description = "Test",
            Specification = "Test",
            Manufacture = "Test",
            CategoryName = "Test",
            Price = 100,
            CurrencyCode = "Test",
            Currency = "Test"
        };

        // Act
        var result = await productService.CreateProductAsync(productRegDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetProductsAsync_Should_GetAll_And_ReturnIEnumerableOfTypeProductDto()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufactureRepository, productPriceRepository, productRepository);
        var productRegDto = new ProductRegDto
        {
            ArticleNumber = "1",
            Title = "Test",
            Description = "Test",
            Specification = "Test",
            Manufacture = "Test",
            CategoryName = "Test",
            Price = 100,
            CurrencyCode = "Test",
            Currency = "Test"
        };
        await productService.CreateProductAsync(productRegDto);

        // Act
        var result = await productService.GetProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetProductAsync_Should_GetOneProduct_And_ReturnOneProductDto()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufactureRepository, productPriceRepository, productRepository);
        var productRegDto = new ProductRegDto
        {
            ArticleNumber = "1",
            Title = "Test",
            Description = "Test",
            Specification = "Test",
            Manufacture = "Test",
            CategoryName = "Test",
            Price = 100,
            CurrencyCode = "Test",
            Currency = "Test"
        };
        await productService.CreateProductAsync(productRegDto);

        var productDto = new ProductDto
        {
            ArticleNumber = productRegDto.ArticleNumber,
            Title = productRegDto.Title,
            Description = productRegDto.Description,
            Specification = productRegDto.Specification,
            Manufacture = productRegDto.Manufacture,
            CategoryName = productRegDto.CategoryName,
            Price = productRegDto.Price,
            CurrencyCode = productRegDto.CurrencyCode,
            Currency = productRegDto.Currency
        };

        // Act
        var result = await productService.GetProductAsync(x => x.ArticleNumber == productDto.ArticleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDto.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public async Task UpdateProductAsync_Should_UpdateExistingProduct_And_ReturnUpdatedProductDto()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufactureRepository, productPriceRepository, productRepository);
        var productRegDto = new ProductRegDto
        {
            ArticleNumber = "1",
            Title = "Test",
            Description = "Test",
            Specification = "Test",
            Manufacture = "Test",
            CategoryName = "Test",
            Price = 100,
            CurrencyCode = "Test",
            Currency = "Test"
        };
        await productService.CreateProductAsync(productRegDto);

        var productDto = new ProductDto
        {
            ArticleNumber = productRegDto.ArticleNumber,
            Title = productRegDto.Title,
            Description = productRegDto.Description,
            Specification = productRegDto.Specification,
            Manufacture = productRegDto.Manufacture,
            CategoryName = productRegDto.CategoryName,
            Price = productRegDto.Price,
            CurrencyCode = productRegDto.CurrencyCode,
            Currency = productRegDto.Currency
        };

        // Act
        productDto.Title = "NewTest";
        productDto.Description = "NewTest";
        productDto.Specification = "NewTest";
        productDto.Manufacture = "NewTest";
        productDto.CategoryName = "NewTest";
        productDto.Price = 200;
        productDto.CurrencyCode = "NewTest";
        productDto.Currency = "NewTest";
        var result = await productService.UpdateProductAsync(productDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productDto.ArticleNumber, result.ArticleNumber);
        Assert.Equal("NewTest", result.Title);
        Assert.Equal("NewTest", result.Description);
        Assert.Equal("NewTest", result.Specification);
        Assert.Equal("NewTest", result.Manufacture);
        Assert.Equal("NewTest", result.CategoryName);
        Assert.Equal(200, result.Price);
        Assert.Equal("NewTest", result.CurrencyCode);
        Assert.Equal("NewTest", result.Currency);
    }

    [Fact]
    public async Task DeleteProductAsync_Should_RemoveOneProduct_And_ReturnTrue()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        IProductService productService = new ProductService(categoryRepository, currencyRepository, manufactureRepository, productPriceRepository, productRepository);
        var productRegDto = new ProductRegDto
        {
            ArticleNumber = "1",
            Title = "Test",
            Description = "Test",
            Specification = "Test",
            Manufacture = "Test",
            CategoryName = "Test",
            Price = 100,
            CurrencyCode = "Test",
            Currency = "Test"
        };
        await productService.CreateProductAsync(productRegDto);

        var productDto = new ProductDto
        {
            ArticleNumber = productRegDto.ArticleNumber,
            Title = productRegDto.Title,
            Description = productRegDto.Description,
            Specification = productRegDto.Specification,
            Manufacture = productRegDto.Manufacture,
            CategoryName = productRegDto.CategoryName,
            Price = productRegDto.Price,
            CurrencyCode = productRegDto.CurrencyCode,
            Currency = productRegDto.Currency
        };

        // Act
        var result = await productService.DeleteProductAsync(productDto);

        // Assert
        Assert.True(result);
    }
}
