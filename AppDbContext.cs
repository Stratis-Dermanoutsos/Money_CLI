namespace Money_CLI;

using Microsoft.EntityFrameworkCore;
using Money_CLI.Models;

public class AppDbContext : DbContext
{
    // Empty constructor but the parameter is needed for Dependency Injection
    // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=/Volumes/Stratis_SSD/PERSONAL/My_database/money.db");
    }
}