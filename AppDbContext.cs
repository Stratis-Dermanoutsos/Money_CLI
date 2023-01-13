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

    /// <summary>
    /// <inheritdoc />
    /// <br />
    /// Overriden to set the default values for the <paramref name="DateIn" /> on creation automatically.
    /// </summary>
    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ChangeBase && e.State == EntityState.Added);

        foreach (var entityEntry in entries)
            ((ChangeBase)entityEntry.Entity).DateIn = DateTime.Now;

        return base.SaveChanges();
    }
}