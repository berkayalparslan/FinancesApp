using FinancesApp.Models;
using FinancesApp.Repositories;
using FinancesApp.Services;
using Moq;

namespace FinancesApp.Tests;


[TestFixture]
public class AccountServiceTests
{
    private Mock<IRepository<Account>> _mockAccountRepository;
    private Mock<IRepository<Currency>> _mockCurrencyRepository;
    private AccountService _accountService;

    [SetUp]
    public void Setup()
    {
        _mockAccountRepository = new Mock<IRepository<Account>>();
        _mockCurrencyRepository = new Mock<IRepository<Currency>>();
        _accountService = new AccountService(_mockAccountRepository.Object);
    }

    [Test]
    public async Task GetAllAccounts_ReturnsList_When_AccountsArePresent()
    {
        var mockCurrency = new Currency { Name = "USD" };
        var mockAccounts = new List<Account>()
        {
            new Account {Name = "Test Account", CurrencyId = mockCurrency.Id},
        };
        _mockAccountRepository?.Setup(repo => repo.GetAllAsync(x => true)).ReturnsAsync(mockAccounts);

        var result = await _accountService.GetAccountsAsync();

        Assert.That(result, Is.TypeOf<List<Account>>());
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task CreateAccount_ReturnsNewAccount_WhenAccountIsCreated()
    {
        var mockCurrencies = new List<Currency>()
        {
            new Currency { Id=1, Name = "USD" },
            new Currency { Id=2, Name = "EUR" },
            new Currency { Id=3, Name = "PLN" }
        };
        var newAccount = new Account { Name = "Test Account", CurrencyId = mockCurrencies[0].Id };
        _mockCurrencyRepository.Setup(repo => repo.GetAllAsync(x => true)).ReturnsAsync(mockCurrencies);
        _mockAccountRepository.Setup(repo => repo.CreateAsync(It.IsAny<Account>())).ReturnsAsync(newAccount);

        var result = await _accountService.CreateAccountAsync(newAccount);

        Assert.That(result, Is.TypeOf<Account>());
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.Name, Is.EqualTo(newAccount.Name));
        Assert.That(result?.CurrencyId, Is.EqualTo(newAccount.CurrencyId));
    }

    [Test]
    public async Task CreateAccount_ReturnsNull_WhenAccountNameIsDuplicate()
    {
        var mockCurrencies = new List<Currency>()
        {
            new Currency { Id=1, Name = "USD" }
        };
        var existingAccount = new Account { Id = 1, Name = "Test Account", CurrencyId = mockCurrencies[0].Id };
        var newAccount = new Account { Name = "Test Account", CurrencyId = mockCurrencies[0].Id };
        var mockAccounts = new List<Account>()
        {
            existingAccount
        };
        
        _mockAccountRepository.Setup(repo => repo.GetAllAsync(x => true)).ReturnsAsync(mockAccounts);

        var result = await _accountService.CreateAccountAsync(newAccount);
        var accounts = await _accountService.GetAccountsAsync();

        Assert.That(result, Is.Null);
        Assert.That(accounts?.Count(), Is.EqualTo(1));
        Assert.That(accounts?.First().Name, Is.EqualTo(existingAccount.Name));
    }
}