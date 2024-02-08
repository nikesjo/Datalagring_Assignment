using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ProductPriceRepository_Tests
{
    private readonly ProductCatalogContext _productCatalogContext =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewProductPrice_And_ReturnProductPrice()
    {
        // Arrange
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",
            Description = "Description",
            Specification = "Specification",
            ManufactureId = manufacture.Id,
            CategoryId = category.Id
        };
        await productRepository.CreateAsync(product);

        var productPrice = new ProductPrice
        {
            ArticleNumber = product.ArticleNumber,
            Price = 100,
            CurrencyCode = "SEK"
        };

        // Act
        var result = await productPriceRepository.CreateAsync(productPrice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.ArticleNumber);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeProductPrice()
    {
        // Arrange
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",
            Description = "Description",
            Specification = "Specification",
            ManufactureId = manufacture.Id,
            CategoryId = category.Id
        };
        await productRepository.CreateAsync(product);

        var productPrice = new ProductPrice
        {
            ArticleNumber = product.ArticleNumber,
            Price = 100,
            CurrencyCode = currency.Code
        };
        await productPriceRepository.CreateAsync(productPrice);

        // Act
        var result = await productPriceRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductPrice>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneProductPrice_And_ReturnOneProductPrice()
    {
        // Arrange
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",
            Description = "Description",
            Specification = "Specification",
            ManufactureId = manufacture.Id,
            CategoryId = category.Id
        };
        await productRepository.CreateAsync(product);

        var productPrice = new ProductPrice
        {
            ArticleNumber = product.ArticleNumber,
            Price = 100,
            CurrencyCode = currency.Code
        };
        await productPriceRepository.CreateAsync(productPrice);

        // Act
        var result = await productPriceRepository.GetAsync(x => x.ArticleNumber == productPrice.ArticleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productPrice.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfProductPriceExists_And_ReturnFound()
    {
        // Arrange
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",
            Description = "Description",
            Specification = "Specification",
            ManufactureId = manufacture.Id,
            CategoryId = category.Id
        };
        await productRepository.CreateAsync(product);

        var productPrice = new ProductPrice
        {
            ArticleNumber = product.ArticleNumber,
            Price = 100,
            CurrencyCode = currency.Code
        };
        await productPriceRepository.CreateAsync(productPrice);

        // Act
        var result = await productPriceRepository.ExistsAsync(x => x.ArticleNumber == productPrice.ArticleNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingProductPrice_And_ReturnUpdatedProductPrice()
    {
        // Arrange
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",
            Description = "Description",
            Specification = "Specification",
            ManufactureId = manufacture.Id,
            CategoryId = category.Id
        };
        await productRepository.CreateAsync(product);

        var productPrice = new ProductPrice
        {
            ArticleNumber = product.ArticleNumber,
            Price = 100,
            CurrencyCode = currency.Code
        };
        await productPriceRepository.CreateAsync(productPrice);

        // Act
        productPrice.Price = 200;
        productPrice.CurrencyCode = "USD";
        var result = await productPriceRepository.UpdateAsync(x => x.ArticleNumber == productPrice.ArticleNumber, productPrice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productPrice.ArticleNumber, result.ArticleNumber);
        Assert.Equal(200, result.Price);
        Assert.Equal("USD", result.CurrencyCode);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneProductPrice_And_ReturnTrue()
    {
        // Arrange
        IProductPriceRepository productPriceRepository = new ProductPriceRepository(_productCatalogContext);
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",
            Description = "Description",
            Specification = "Specification",
            ManufactureId = manufacture.Id,
            CategoryId = category.Id
        };
        await productRepository.CreateAsync(product);

        var productPrice = new ProductPrice
        {
            ArticleNumber = product.ArticleNumber,
            Price = 100,
            CurrencyCode = currency.Code
        };
        await productPriceRepository.CreateAsync(productPrice);

        // Act
        var result = await productPriceRepository.DeleteAsync(x => x.ArticleNumber == productPrice.ArticleNumber);

        // Assert
        Assert.True(result);
    }
}
