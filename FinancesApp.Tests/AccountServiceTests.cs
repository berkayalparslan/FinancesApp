using FinancesApp.Models;
using FinancesApp.Repositories;
using FinancesApp.Services;
using Moq;

namespace FinancesApp.Tests;


[TestFixture]
public class AccountServiceTests
{
    private Mock<IRepository<Account>> _mockAccountRepository;
    private AccountService _accountService;
    [SetUp]
    public void Setup()
    {
        _mockAccountRepository = new Mock<IRepository<Account>>();
        _accountService = new AccountService(_mockAccountRepository.Object);
    }

    [Test]
    public async Task GET_Accounts_Returns_OK_When_Accounts_Are_Present()
    {
        var mockCurrency = new Currency { Name = "USD" };
        var mockAccounts = new List<Account>()
        {
            new Account {Name = "Test Account", CurrencyId = mockCurrency.Id},   
        };
        _mockAccountRepository.Setup(repo => repo.GetAllAsync(x=>true)).ReturnsAsync(mockAccounts);

        var result = await _accountService.GetAccountsAsync();

        Assert.That(result, Is.TypeOf<List<Account>>());
        Assert.That(result, Is.Not.Null);
        // Assert.That(result.Value.Count, Is.EqualTo(mockCurrencies.Count));
    }
}