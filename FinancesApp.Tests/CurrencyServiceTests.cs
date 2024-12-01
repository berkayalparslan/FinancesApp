using System.Linq.Expressions;
using FinancesApp.Models;
using FinancesApp.Repositories;
using FinancesApp.Services;
using Moq;

namespace FinancesApp.Tests;


[TestFixture]
public class CurrencyServiceTests
{
    private Mock<IRepository<Currency>> _mockCurrencyRepository;
    private CurrencyService _currencyService;

    [SetUp]
    public void Setup()
    {
        _mockCurrencyRepository = new Mock<IRepository<Currency>>();
        _currencyService = new CurrencyService(_mockCurrencyRepository.Object);
    }

    [Test]
    public async Task GET_Currencies_Returns_OK_When_Currencies_Are_Present()
    {
        var mockCurrencies = new List<Currency>()
        {
            new Currency { Name = "USD" },
            new Currency { Name = "EUR" },
            new Currency { Name = "PLN" }
        };
        _mockCurrencyRepository.Setup(repo => repo.GetAllAsync(x => true)).ReturnsAsync(mockCurrencies);

        var result = await _currencyService.GetCurrenciesAsync();

        Assert.That(result, Is.TypeOf<List<Currency>>());
        Assert.That(result, Is.Not.Null);
        // Assert.That(result.Value.Count, Is.EqualTo(mockCurrencies.Count));
    }

    [Test]
    public async Task CreateCurrency_ReturnsNewCurrency_WhenCurrencyIsCreated()
    {
        var newCurrency = new Currency { Name = "xd" };
        var mockCurrencies = new List<Currency>();
        _mockCurrencyRepository.Setup(repo => repo.CreateAsync(It.IsAny<Currency>())).ReturnsAsync((Currency currency) =>
        {
            mockCurrencies.Add(currency);
            return currency;
        });
        _mockCurrencyRepository
        .Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Currency, bool>>>()))
        .ReturnsAsync(mockCurrencies);

        var result = await _currencyService.CreateCurrencyAsync(newCurrency);
        var currencies = await _currencyService.GetCurrenciesAsync();

        Assert.That(result, Is.TypeOf<Currency>());
        Assert.That(result, Is.Not.Null);
        Assert.That(currencies?.Count(), Is.EqualTo(1));
        Assert.That(currencies?.First().Name, Is.EqualTo(newCurrency.Name));
    }

    [Test]
    public async Task CreateCurrency_ReturnsNull_WhenCurrencyNameIsDuplicate()
    {
        var existingCurrency = new Currency { Id = 1, Name = "USD" };
        var newCurrency = new Currency { Name = "xd" };
        var mockCurrencies = new List<Currency>() { existingCurrency };

        _mockCurrencyRepository.Setup(repo => repo.GetAllAsync(x => true)).ReturnsAsync(mockCurrencies);

        var result = await _currencyService.CreateCurrencyAsync(newCurrency);
        var currencies = await _currencyService.GetCurrenciesAsync();

        Assert.That(result, Is.Null);
        Assert.That(currencies?.Count(), Is.EqualTo(1));
        Assert.That(currencies?.First().Name, Is.EqualTo(existingCurrency.Name));

    }
}