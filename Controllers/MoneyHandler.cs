namespace Money_CLI.Controllers;

using Microsoft.EntityFrameworkCore;
using Money_CLI.Models;

public static class MoneyHandler
{
    #region Get total monthly income and expenses
    /// <summary>
    /// Returns the sum of all expenses for specified month.
    /// </summary>
    public static double TotalMonthlyExpenses(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Month == month && i.Year == year)
                            .Sum(i => i.Amount);
        }
    }

    /// <summary>
    /// Returns the sum of all income for specified month.
    /// </summary>
    public static double TotalMonthlyIncome(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Incomes
                            .Where(i => i.Month == month && i.Year == year)
                            .Sum(i => i.Amount);
        }
    }
    #endregion

    #region All monthly income and expenses
    /// <summary>
    /// Returns an List containing all monthly expenses.
    /// </summary>
    public static List<Expense> AllMonthlyExpenses(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Month == month && i.Year == year)
                            .OrderBy(i => i.Day)
                            .ThenBy(i => i.Title).ToList();
        }
    }

    /// <summary>
    /// Returns an List containing all monthly expenses ordered by Id.
    /// </summary>
    public static List<Expense> AllMonthlyExpensesById(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Month == month && i.Year == year)
                            .Ordered()
                            .ToList();
        }
    }

    /// <summary>
    /// Returns an List containing all monthly income.
    /// </summary>
    public static List<Income> AllMonthlyIncome(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Incomes
                            .Where(i => i.Month == month && i.Year == year)
                            .OrderBy(i => i.Day)
                            .ThenBy(i => i.Title).ToList();
        }
    }

    /// <summary>
    /// Returns an List containing all monthly income ordered by Id.
    /// </summary>
    public static List<Income> AllMonthlyIncomeById(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Incomes
                            .Where(i => i.Month == month && i.Year == year)
                            .Ordered()
                            .ToList();
        }
    }
    #endregion

    #region All income/expenses based on month
    /// <summary>
    /// Returns all expenses for specified month.
    /// </summary>
    public static List<Expense> AllExpensesOnMonth(int month)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Month == month)
                            .Ordered()
                            .ToList();
        }
    }

    /// <summary>
    /// Returns all income for specified month.
    /// </summary>
    public static List<Income> AllIncomeOnMonth(int month)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Incomes
                            .Where(i => i.Month == month)
                            .Ordered()
                            .ToList();
        }
    }
    #endregion

    #region All income/expenses based on year
    /// <summary>
    /// Returns all expenses for specified year.
    /// </summary>
    public static List<Expense> AllExpensesOnYear(int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Year == year)
                            .Ordered()
                            .ToList();
        }
    }

    /// <summary>
    /// Returns all income for specified year.
    /// </summary>
    public static List<Income> AllIncomeOnYear(int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Incomes
                            .Where(i => i.Year == year)
                            .Ordered()
                            .ToList();
        }
    }
    #endregion

    #region  Return all income/expenses.
    /// <summary>
    /// Returns all expenses.
    /// </summary>
    public static List<Expense> AllExpenses()
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Ordered()
                            .ToList();
        }
    }

    /// <summary>
    /// Returns all income.
    /// </summary>
    public static List<Income> AllIncome()
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Incomes
                            .Ordered()
                            .ToList();
        }
    }
    #endregion

    /// <summary>
    /// Adds a change to the database.
    /// <br />
    /// <paramref name="change"/>
    /// <param name="change">The change to add.</param>
    /// </summary>
    public static void AddChange<T>(T change) where T : ChangeBase
    {
        using (AppDbContext context = new AppDbContext())
        {
            context.Set<T>().Add(change);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Removes a change from the database.
    /// <br />
    /// <paramref name="id" />
    /// <param name="id">The id of the change to remove.</param>
    /// </summary>
    public static void RemoveChange<T>(int id) where T : ChangeBase
    {
        using (AppDbContext context = new AppDbContext())
        {
            T? change = context.Set<T>().Find(id);

            if (change == null)
                throw new Exception($"{typeof(T).Name} not found.");

            try {
                context.Set<T>().Remove(change);
                context.SaveChanges();
            } catch (Exception) {
                throw new Exception($"Could not remove {typeof(T).Name.ToLower()}.");
            }
        }
    }
}