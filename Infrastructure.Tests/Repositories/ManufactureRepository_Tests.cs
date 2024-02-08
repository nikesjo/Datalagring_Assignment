using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class ManufactureRepository_Tests
{
    private readonly ProductCatalogContext _productCatalogContext =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewManufacture_And_ReturnManufacture()
    {
        // Arrange
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };

        // Act
        var result = await manufactureRepository.CreateAsync(manufacture);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeManufacture()
    {
        // Arrange
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        // Act
        var result = await manufactureRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Manufacture>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneManufacture_And_ReturnOneManufacture()
    {
        // Arrange
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        // Act
        var result = await manufactureRepository.GetAsync(x => x.Id == manufacture.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(manufacture.Id, result.Id);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfManufactureExists_And_ReturnFound()
    {
        // Arrange
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        // Act
        var result = await manufactureRepository.ExistsAsync(x => x.Id == manufacture.Id);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingManufacture_And_ReturnUpdatedManufacture()
    {
        // Arrange
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        // Act
        manufacture.Manufacture1 = "NewTest";
        var result = await manufactureRepository.UpdateAsync(x => x.Id == manufacture.Id, manufacture);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(manufacture.Id, result.Id);
        Assert.Equal("NewTest", result.Manufacture1);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneManufacture_And_ReturnTrue()
    {
        // Arrange
        IManufactureRepository manufactureRepository = new ManufactureRepository(_productCatalogContext);
        var manufacture = new Manufacture
        {
            Manufacture1 = "Test"
        };
        await manufactureRepository.CreateAsync(manufacture);

        // Act
        var result = await manufactureRepository.DeleteAsync(x => x.Id == manufacture.Id);

        // Assert
        Assert.True(result);
    }
}
