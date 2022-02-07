namespace Money_CLI.CommandLine;

using Money_CLI.Controllers;
using Money_CLI.Models;
using Money_CLI.Models.Enums;

public class Handlers
{
    public static void ExecuteRoot(
        string SetExport,
        string SetDatabase,
        string SetCurrency
    ) {
        if (SetExport != null) {
            if (Directory.Exists(SetExport))
                SystemVariables.ExportFolder = SetExport;
            else
                GenericController.PrintError("Export folder does not exist.");
        }

        if (SetDatabase != null) {
            if (Directory.Exists(SetDatabase)) {
                string oldPath = @$"{SystemVariables.DatabaseFolder}"; // We keep the old path in case we need to revert
                string oldPathFull = @$"{oldPath}money.db";

                if (File.Exists(oldPathFull)) {
                    try {
                        SystemVariables.DatabaseFolder = SetDatabase;

                        string newPath = @$"{SystemVariables.DatabaseFolder}money.db";
                        GenericController.PrintWarning($"The database will be moved to the new path...");

                        File.Move(@$"{oldPath}money.db", newPath, true);
                        GenericController.PrintSuccess("The database was moved successfully.");
                    } catch (Exception) {
                        GenericController.PrintWarning("The database could not be moved.\nReverting back to the old path...");
                        SystemVariables.DatabaseFolder = oldPath;
                    }
                } else {
                    GenericController.PrintError("The database does not exist.");
                }
            } else
                GenericController.PrintError("Database folder does not exist.");
        }

        // TODO: Handle SetCurrency.
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
    ) {
        ChangeBase change = new ChangeBase();

        // Handle all properties
        if (Title != null)
            change.Title = Title;

        if (Amount != 0)
            change.Amount = Amount;

        if (Year != 0)
            change.Year = Year;

        if (Month != 0)
            change.Month = Month;

        if (Day != 0)
            change.Day = Day;

        if (Comment != null)
            change.Comment = Comment;

        /* Add the change to the database
        ! Only 1 change type can be set at a time. */
        if (Expense) {
            Expense expense = new Expense(change);
            try {
                MoneyHandler.AddExpense(expense);
                GenericController.PrintSuccess("Expense added successfully.");
            } catch (Exception) {
                GenericController.PrintError("Could not add expense.");
            }
        } else if (Income) {
            Income income = new Income(change);
            try {
                MoneyHandler.AddIncome(income);
                GenericController.PrintSuccess("Income added successfully.");
            } catch (Exception) {
                GenericController.PrintError("Could not add income.");
            }
        } else
            GenericController.PrintError("You must specify whether the change is an expense or income.");
    }

    public static void ExecuteExport(
        bool Expense,
        bool Income,
        int Month,
        int Year
    ) {
        if (Expense) {
            if (!FileHandler.Export(ChangeType.Expense, Month, Year))
                GenericController.PrintError("Could not export the expenses.");

            return;
        }

        if (Income) {
            if (!FileHandler.Export(ChangeType.Income, Month, Year))
                GenericController.PrintError("Could not export the income.");

            return;
        }

        GenericController.PrintError("Provide a valid option to export.");
    }

    public static void ExecuteList(
        bool Expense,
        bool Income,
        int Month,
        int Year
    ) {
        if (Expense) {
            try {
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

                if (expenses.Count == 0) {
                    GenericController.PrintWarning("There are no expenses to list.");
                    return;
                }

                foreach (Expense expense in expenses)
                    GenericController.PrintDefault(expense.ToString("list"));
            } catch (Exception) {
                GenericController.PrintError("Could not list the expenses.");
            }

            return;
        }

        if (Income) {
            try {
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

                if (incomes.Count == 0) {
                    GenericController.PrintWarning("There is no income to list.");
                    return;
                }

                foreach (Income income in incomes)
                    GenericController.PrintDefault(income.ToString("list"));
            } catch (Exception) {
                GenericController.PrintError("Could not list the income.");
            }

            return;
        }

        GenericController.PrintError("Provide a valid option to list.");
    }

    public static void ExecuteRemove(
        bool Expense,
        bool Income,
        int Id
    ) {
        if (Id == 0) {
            GenericController.PrintError("Provide a valid ID.");
            return;
        }

        if (Expense) {
            try {
                MoneyHandler.RemoveExpense(Id);
                GenericController.PrintSuccess("Expense removed successfully.");
            } catch (Exception) {
                GenericController.PrintError("Could not remove the expense.");
            }

            return;
        }

        if (Income) {
            try {
                MoneyHandler.RemoveIncome(Id);
                GenericController.PrintSuccess("Income removed successfully.");
            } catch (Exception) {
                GenericController.PrintError("Could not remove the income.");
            }

            return;
        }

        GenericController.PrintError("Provide a valid option to remove.");
    }
}