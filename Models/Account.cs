using System.ComponentModel.DataAnnotations.Schema;

namespace FinancesApp.Models;

public class Account
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [ForeignKey("Currency")]
    public required int CurrencyId {get;set;}
    public Currency Currency { get; set; } = null!;

}