using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class CategoryRepository_Tests
{
    private readonly ProductCatalogContext _productCatalogContext =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewCategory_And_ReturnCategory()
    {
        // Arrange
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        var category = new Category
        {
            CategoryName = "Test",
        };

        // Act
        var result = await categoryRepository.CreateAsync(category);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeCategory()
    {
        // Arrange
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        // Act
        var result = await categoryRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneCategory_And_ReturnOneCategory()
    {
        // Arrange
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        // Act
        var result = await categoryRepository.GetAsync(x => x.Id == category.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfCategoryExists_And_ReturnFound()
    {
        // Arrange
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        // Act
        var result = await categoryRepository.ExistsAsync(x => x.Id == category.Id);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingCategory_And_ReturnUpdatedCategory()
    {
        // Arrange
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        // Act
        category.CategoryName = "NewTest";
        var result = await categoryRepository.UpdateAsync(x => x.Id == category.Id, category);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
        Assert.Equal("NewTest", result.CategoryName);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneCategory_And_ReturnTrue()
    {
        // Arrange
        ICategoryRepository categoryRepository = new CategoryRepository(_productCatalogContext);
        var category = new Category
        {
            CategoryName = "Test"
        };
        await categoryRepository.CreateAsync(category);

        // Act
        var result = await categoryRepository.DeleteAsync(x => x.Id == category.Id);

        // Assert
        Assert.True(result);
    }
}
