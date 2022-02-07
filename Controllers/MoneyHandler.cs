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
    /// Returns an List containing all monthly expenses ordered by Id.
    /// </summary>
    public static List<Expense> AllMonthlyExpensesById(int month, int year)
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .Where(i => i.Month == month && i.Year == year)
                            .OrderBy(i => i.Id)
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
                            .OrderBy(i => i.Id)
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
                            .OrderBy(i => i.Id)
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
                            .OrderBy(i => i.Id)
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
                            .OrderBy(i => i.Id)
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
                            .OrderBy(i => i.Id)
                            .ToList();
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

    #region  Return all income/expenses.
    /// <summary>
    /// Returns all expenses.
    /// </summary>
    public static List<Expense> AllExpenses()
    {
        using (AppDbContext context = new AppDbContext())
        {
            return context.Expenses
                            .OrderBy(i => i.Id)
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
                            .OrderBy(i => i.Id)
                            .ToList();
        }
    }
    #endregion

    #region Remove income/expense
    /// <summary>
    /// Removes an expense from the database.
    /// </summary>
    public static void RemoveExpense(int Id)
    {
        using (AppDbContext context = new AppDbContext())
        {
            Expense expense = context.Expenses.Single(i => i.Id == Id);

            context.Expenses.Remove(expense);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Removes an income from the database.
    /// </summary>
    public static void RemoveIncome(int Id)
    {
        using (AppDbContext context = new AppDbContext())
        {
            Income income = context.Incomes.Single(i => i.Id == Id);

            context.Incomes.Remove(income);
            context.SaveChanges();
        }
    }
    #endregion
}