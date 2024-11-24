using Microsoft.EntityFrameworkCore;

namespace FinancesApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Account> Accounts { get; set; }
}