using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class CurrencyRepository_Tests
{
    private readonly ProductCatalogContext _productCatalogContext =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateAsync_Should_CreateANewCurrency_And_ReturnCurrency()
    {
        // Arrange
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };

        // Act
        var result = await currencyRepository.CreateAsync(currency);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("SEK", result.Code);
    }

    [Fact]
    public async Task GetAsync_Should_GetAll_And_ReturnIEnumerableOfTypeCurrency()
    {
        // Arrange
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);

        // Act
        var result = await currencyRepository.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Currency>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_Should_GetOneCurrency_And_ReturnOneCurrency()
    {
        // Arrange
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);

        // Act
        var result = await currencyRepository.GetAsync(x => x.Code == currency.Code);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(currency.Code, result.Code);
    }

    [Fact]
    public async Task ExistsAsync_Should_CheckIfCurrencyExists_And_ReturnFound()
    {
        // Arrange
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);

        // Act
        var result = await currencyRepository.ExistsAsync(x => x.Code == currency.Code);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingCurrency_And_ReturnUpdatedCurrency()
    {
        // Arrange
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);

        // Act
        currency.Currency1 = "US Dollar";
        var result = await currencyRepository.UpdateAsync(x => x.Code == currency.Code, currency);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(currency.Code, result.Code);
        Assert.Equal("US Dollar", result.Currency1);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneCurrency_And_ReturnTrue()
    {
        // Arrange
        ICurrencyRepository currencyRepository = new CurrencyRepository(_productCatalogContext);
        var currency = new Currency
        {
            Code = "SEK",
            Currency1 = "Svensk Krona"
        };
        await currencyRepository.CreateAsync(currency);

        // Act
        var result = await currencyRepository.DeleteAsync(x => x.Code == currency.Code);

        // Assert
        Assert.True(result);
    }
}
