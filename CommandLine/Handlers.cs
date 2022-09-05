namespace Money_CLI.CommandLine;

using Money_CLI.Controllers;
using Money_CLI.Models;
using Money_CLI.Models.Enums;
using Serilog;

public class Handlers
{
    public static void ExecuteRoot(
        string SetExport,
        string SetDatabase,
        string SetCurrency
    )
    {
        if (SetExport != null)
        {
            if (Directory.Exists(SetExport))
                SystemVariables.ExportFolder = SetExport;
            else
                Log.Error("Export folder does not exist.");
        }

        if (SetDatabase != null)
        {
            if (Directory.Exists(SetDatabase))
            {
                string oldPath = @$"{SystemVariables.DatabaseFolder}"; // We keep the old path in case we need to revert
                string oldPathFull = @$"{oldPath}money.db";

                if (File.Exists(oldPathFull))
                {
                    try
                    {
                        SystemVariables.DatabaseFolder = SetDatabase;

                        string newPath = @$"{SystemVariables.DatabaseFolder}money.db";
                        Log.Warning("The database will be moved to the new path...");

                        File.Move(@$"{oldPath}money.db", newPath, true);
                        Log.Information("The database was moved successfully.");
                    }
                    catch (Exception)
                    {
                        Log.Warning("The database could not be moved.\nReverting back to the old path...");
                        SystemVariables.DatabaseFolder = oldPath;
                    }
                }
                else
                {
                    Log.Error("The database does not exist.");
                }
            }
            else
                Log.Error("Database folder does not exist.");
        }

        if (SetCurrency != null)
        {
            switch (SetCurrency)
            {
                case "EUR": // Euro
                    SystemVariables.Currency = "fr-FR";
                    break;
                case "GBP": // British Pound
                    SystemVariables.Currency = "en-GB";
                    break;
                case "JPY": // Japanese Yen
                    SystemVariables.Currency = "ja-JP";
                    break;
                case "USD": // US Dollar
                    SystemVariables.Currency = "en-US";
                    break;
                default:
                    Log.Error("Currency is not supported.");
                    break;
            }
        }
    }

    public static void ExecuteAdd(
        bool Expense,
        bool Income,
        string Title,
        double Amount,
        int Year,
        int Month,
        int Day,
        string Comment
    )
    {
        ChangeBase change = new ChangeBase()
                            .SetTitle(Title ?? string.Empty)
                            .SetAmount(Amount)
                            .SetComment(Comment ?? string.Empty);

        if (Year != 0)
            change.SetYear(Year);

        if (Month != 0)
            change.SetMonth(Month);

        if (Day != 0)
            change.SetDay(Day);

        /* Add the change to the database
        ! Only 1 change type can be set at a time. */
        if (Expense)
        {
            Expense expense = new Expense(change);
            try
            {
                MoneyHandler.AddExpense(expense);
                Log.Information("Expense added successfully.");
            }
            catch (Exception)
            {
                Log.Error("Could not add expense.");
            }
        }
        else if (Income)
        {
            Income income = new Income(change);
            try
            {
                MoneyHandler.AddIncome(income);
                Log.Information("Income added successfully.");
            }
            catch (Exception)
            {
                Log.Error("Could not add income.");
            }
        }
        else
            Log.Error("You must specify whether the change is an expense or income.");
    }

    public static void ExecuteExport(
        bool Expense,
        bool Income,
        int Month,
        int Year
    )
    {
        if (Expense)
        {
            FileHandler.Export(ChangeType.Expense, Month, Year);
            return;
        }

        if (Income)
        {
            FileHandler.Export(ChangeType.Income, Month, Year);
            return;
        }

        Log.Error("Provide a valid option to export.");
    }

    public static void ExecuteList(
        bool Expense,
        bool Income,
        int Month,
        int Year
    )
    {
        if (Expense)
        {
            try
            {
                List<Expense> expenses;

                // Only get the expenses that are specified by the user, or all.
                if (GenericController.MonthIsValid(Month) && GenericController.YearIsValid(Year))
                    expenses = MoneyHandler.AllMonthlyExpensesById(Month, Year);
                else if (GenericController.MonthIsValid(Month))
                    expenses = MoneyHandler.AllExpensesOnMonth(Month);
                else if (GenericController.YearIsValid(Year))
                    expenses = MoneyHandler.AllExpensesOnYear(Year);
                else
                    expenses = MoneyHandler.AllExpenses();

                if (expenses.Count == 0)
                {
                    Log.Warning("There are no expenses to list.");
                    return;
                }

                foreach (Expense expense in expenses)
                    Log.Information(expense.ToString("list"));
            }
            catch (Exception)
            {
                Log.Error("Could not list the expenses.");
            }

            return;
        }

        if (Income)
        {
            try
            {
                List<Income> incomes;

                // Only get the income that are specified by the user, or all.
                if (GenericController.MonthIsValid(Month) && GenericController.YearIsValid(Year))
                    incomes = MoneyHandler.AllMonthlyIncomeById(Month, Year);
                else if (GenericController.MonthIsValid(Month))
                    incomes = MoneyHandler.AllIncomeOnMonth(Month);
                else if (GenericController.YearIsValid(Year))
                    incomes = MoneyHandler.AllIncomeOnYear(Year);
                else
                    incomes = MoneyHandler.AllIncome();

                if (incomes.Count == 0)
                {
                    Log.Warning("There is no income to list.");
                    return;
                }

                foreach (Income income in incomes)
                    Log.Information(income.ToString("list"));
            }
            catch (Exception)
            {
                Log.Error("Could not list the income.");
            }

            return;
        }

        Log.Error("Provide a valid option to list.");
    }

    public static void ExecuteRemove(
        bool Expense,
        bool Income,
        int Id
    )
    {
        if (Id == 0)
        {
            Log.Error("Provide a valid ID.");
            return;
        }

        if (Expense)
        {
            try
            {
                MoneyHandler.RemoveExpense(Id);
                Log.Information("Expense removed successfully.");
            }
            catch (Exception)
            {
                Log.Error("Could not remove the expense.");
            }

            return;
        }

        if (Income)
        {
            try
            {
                MoneyHandler.RemoveIncome(Id);
                Log.Information("Income removed successfully.");
            }
            catch (Exception)
            {
                Log.Error("Could not remove the income.");
            }

            return;
        }

        Log.Error("Provide a valid option to remove.");
    }
}