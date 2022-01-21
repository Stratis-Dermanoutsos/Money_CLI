namespace Money_CLI.Controllers;

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
    #endregion

    #region Add new income/expense
    /// <summary>
    /// Adds a new expense to the database.
    /// </summary>
    public static void AddExpense(Expense expense)
    {
        using (AppDbContext context = new AppDbContext())
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Adds a new income to the database.
    /// </summary>
    public static void AddIncome(Income income)
    {
        using (AppDbContext context = new AppDbContext())
        {
            context.Incomes.Add(income);
            context.SaveChanges();
        }
    }
    #endregion
}