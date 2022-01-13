namespace Money_CLI;

using Microsoft.EntityFrameworkCore;
using Money_CLI.Models;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public class AppDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@$"Data Source={SystemVariables.DatabaseFolder}money.db");
    }
}