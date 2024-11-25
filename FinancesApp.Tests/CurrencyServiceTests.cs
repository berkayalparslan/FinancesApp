using FinancesApp.Models;
using FinancesApp.Repositories;
using FinancesApp.Services;
using Moq;

namespace FinancesApp.Tests;


[TestFixture]
public class CurrencyServiceTests
{
    private Mock<IRepository<Currency>> _mockRepository;
    private CurrencyService _currencyService;
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IRepository<Currency>>();
        _currencyService = new CurrencyService(_mockRepository.Object);
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
        _mockRepository.Setup(repo => repo.GetAllAsync(x=>true)).ReturnsAsync(mockCurrencies);

        var result = await _currencyService.GetCurrenciesAsync();

        Assert.That(result, Is.TypeOf<List<Currency>>());
        Assert.That(result, Is.Not.Null);
        // Assert.That(result.Value.Count, Is.EqualTo(mockCurrencies.Count));
    }
}