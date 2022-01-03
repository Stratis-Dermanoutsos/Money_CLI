namespace Money_CLI.Controllers;

using Money_CLI.Models;

public static class MoneyHandler
{
    #region Get total monthly income and expenses
    public static double TotalMonthlyExpenses(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Month == month && i.Year == year)
                            .Sum(i => i.Amount);
        }
    }

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
    public static IOrderedQueryable<Expense> AllMonthlyExpenses(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Month == month && i.Year == year)
                            .OrderBy(i => i.Day)
                            .ThenBy(i => i.Title);
        }
    }

    public static IOrderedQueryable<Income> AllMonthlyIncome(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Incomes
                            .Where(i => i.Month == month && i.Year == year)
                            .OrderBy(i => i.Day)
                            .ThenBy(i => i.Title);
        }
    }
    #endregion
}