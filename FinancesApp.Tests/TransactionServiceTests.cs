using FinancesApp.Models;
using FinancesApp.Repositories;
using FinancesApp.Services;
using Moq;

namespace FinancesApp.Tests;


[TestFixture]
public class TransactionServiceTests
{
    private Mock<IRepository<Transaction>> _mockTransactionRepository;
    private TransactionService _transactionService;
    [SetUp]
    public void Setup()
    {
        _mockTransactionRepository = new Mock<IRepository<Transaction>>();
        _transactionService = new TransactionService(_mockTransactionRepository.Object);
    }

    [Test]
    public async Task GET_Transactions_Returns_OK_When_Transactions_Are_Present()
    {
        var mockCurrency = new Currency { Name = "USD" };
        var mockAccount = new Account { Name = "Test Account", CurrencyId = mockCurrency.Id };
        var mockTransactions = new List<Transaction>()
        {
            new Transaction { AccountId = mockAccount.Id, Amount = 20, Date= DateTime.UtcNow, Type= TransactionType.Income},
            
            new Transaction { AccountId =  mockAccount.Id, Amount = 40, Date= DateTime.UtcNow, Type= TransactionType.Outcome},
        };
        _mockTransactionRepository.Setup(repo => repo.GetAllAsync(x=>true)).ReturnsAsync(mockTransactions);

        var result = await _transactionService.GetTransactionsAsync();

        Assert.That(result, Is.TypeOf<List<Transaction>>());
        Assert.That(result, Is.Not.Null);
    }
}