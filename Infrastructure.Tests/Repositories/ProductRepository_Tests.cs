using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ProductRepository_Tests
{
    private readonly ProductCatalogContext _productCatalogContext =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewProduct_And_ReturnProduct()
    {
        // Arrange
        IProductRepository productRepository = new ProductRepository(_productCatalogContext);
        var product = new Product
        {
            ArticleNumber = "1",
            Title = "Title",
            Description = "Description",
            Specification = "Specification",
            ManufactureId = 1,
            CategoryId = 1
        };

        // Act
        var result = await productRepository.CreateAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.ArticleNumber);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeProductEntity()
    {
        // Arrange
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

        // Act
        var result = await productRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneProduct_And_ReturnOneProduct()
    {
        // Arrange
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

        // Act
        var result = await productRepository.GetAsync(x => x.ArticleNumber == product.ArticleNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfProductExists_And_ReturnFound()
    {
        // Arrange
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

        // Act
        var result = await productRepository.ExistsAsync(x => x.ArticleNumber == product.ArticleNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingProduct_And_ReturnUpdatedProduct()
    {
        // Arrange
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

        // Act
        product.Title = "NewTitle";
        product.Description = "NewDescription";
        product.Specification = "NewSpecification";
        var result = await productRepository.UpdateAsync(x => x.ArticleNumber == product.ArticleNumber, product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.ArticleNumber, result.ArticleNumber);
        Assert.Equal("NewTitle", result.Title);
        Assert.Equal("NewDescription", result.Description);
        Assert.Equal("NewSpecification", result.Specification);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneProduct_And_ReturnTrue()
    {
        // Arrange
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

        // Act
        var result = await productRepository.DeleteAsync(x => x.ArticleNumber == product.ArticleNumber);

        // Assert
        Assert.True(result);
    }
}
