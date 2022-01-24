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
}