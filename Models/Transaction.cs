using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FinancesApp.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionType
{
    [EnumMember(Value = "income")]
    Income,
    [EnumMember(Value = "outcome")]
    Outcome
}

public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Name { get; set; }
    public required decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public required int AccountId { get; set; }
    public Account Account { get; set; } = null!;
}