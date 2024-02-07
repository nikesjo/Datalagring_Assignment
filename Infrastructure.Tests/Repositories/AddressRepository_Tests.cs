using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class AddressRepository_Tests
{
    private readonly UserContext _userContext =
        new(new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewAddressEntity_And_ReturnAddressEntity()
    {
        // Arrange
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        var addressEntity = new AddressEntity
        {
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City"
        };

        // Act
        var result = await addressRepository.CreateAsync(addressEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeAddressEntity()
    {
        // Arrange
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        var addressEntity = new AddressEntity
        {
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City"
        };
        await addressRepository.CreateAsync(addressEntity);

        // Act
        var result = await addressRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneAddress_And_ReturnOneAddress()
    {
        // Arrange
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        var addressEntity = new AddressEntity
        {
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City"
        };
        await addressRepository.CreateAsync(addressEntity);

        // Act
        var result = await addressRepository.GetAsync(x => x.Id == addressEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addressEntity.Id, result.Id);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfAddressEntityExists_And_ReturnFound()
    {
        // Arrange
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        var addressEntity = new AddressEntity
        {
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City"
        };
        await addressRepository.CreateAsync(addressEntity);

        // Act
        var result = await addressRepository.ExistsAsync(x => x.Id == addressEntity.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingAddressEntity_And_ReturnUpdatedAddressEntity()
    {
        // Arrange
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        var addressEntity = new AddressEntity
        {
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City"
        };
        await addressRepository.CreateAsync(addressEntity);

        // Act
        addressEntity.StreetName = "NewStreetName";
        addressEntity.PostalCode = "54321";
        addressEntity.City = "NewCity";
        var result = await addressRepository.UpdateAsync(x => x.Id == addressEntity.Id, addressEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addressEntity.Id, result.Id);
        Assert.Equal("NewStreetName", result.StreetName);
        Assert.Equal("54321", result.PostalCode);
        Assert.Equal("NewCity", result.City);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneAddress_And_ReturnTrue()
    {
        // Arrange
        IAddressRepository addressRepository = new AddressRepository(_userContext);
        var addressEntity = new AddressEntity
        {
            StreetName = "StreetName",
            PostalCode = "12345",
            City = "City"
        };
        await addressRepository.CreateAsync(addressEntity);

        // Act
        var result = await addressRepository.DeleteAsync(x => x.Id == addressEntity.Id);

        // Assert
        Assert.True(result);
    }
}
