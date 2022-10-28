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
    ) {
        if (SetExport != null) {
            if (Directory.Exists(SetExport))
                SystemVariables.ExportFolder = SetExport;
            else
                Log.Error("Export folder does not exist.");
        }

        if (SetDatabase != null) {
            if (Directory.Exists(SetDatabase)) {
                string oldPath = @$"{SystemVariables.DatabaseFolder}"; // We keep the old path in case we need to revert
                string oldPathFull = @$"{oldPath}money.db";

                if (File.Exists(oldPathFull)) {
                    try {
                        SystemVariables.DatabaseFolder = SetDatabase;

                        string newPath = @$"{SystemVariables.DatabaseFolder}money.db";
                        Log.Warning("The database will be moved to the new path...");

                        File.Move(@$"{oldPath}money.db", newPath, true);
                        Log.Information("The database was moved successfully.");
                    } catch (Exception) {
                        Log.Warning("The database could not be moved.\nReverting back to the old path...");
                        SystemVariables.DatabaseFolder = oldPath;
                    }
                } else {
                    Log.Error("The database does not exist.");
                }
            } else
                Log.Error("Database folder does not exist.");
        }

        if (SetCurrency != null) {
            switch (SetCurrency) {
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

    /// <summary>
    /// Command to add a change.
    /// <br />
    /// <paramref name="Expense" />
    /// <param name="Expense">boolean value that indicates if the change is expense.</param>
    /// <br />
    /// <paramref name="Income" /> 
    /// <param name="Income">boolean value that indicates if the change is income.</param>
    /// <br />
    /// <paramref name="Title" /> 
    /// <param name="Title">Set the title for the change to add.</param>
    /// <br />
    /// <paramref name="Amount" /> 
    /// <param name="Amount">Set the amount of money for the change to add.</param>
    /// <br />
    /// <paramref name="Day" /> 
    /// <param name="Day">Specify day for the change to add.</param>
    /// <br />
    /// <paramref name="Month" /> 
    /// <param name="Month">Specify month for the change to add.</param>
    /// <br />
    /// <paramref name="Year" /> 
    /// <param name="Year">Specify year for the change to add.</param>
    /// <br />
    /// <paramref name="Comment" /> 
    /// <param name="Comment">Add a comment for the change to add.</param>
    /// </summary>
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
        //* Validate type
        if (Expense && Income)
            throw new Exception("You can add either an expense or income, not both.");
        else if (!Expense && !Income)
            throw new Exception("Provide a valid type to add.");
        String type = Expense ? "Expense" : "Income";

        //* Build the object
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

        try {
            //* Add change
            if (Expense)
                MoneyHandler.AddChange<Expense>(new Expense(change));
            else
                MoneyHandler.AddChange<Income>(new Income(change));
            Log.Information($"{type} added successfully.");
        } catch (Exception) {
            Log.Error($"Could not add {type.ToLower()}.");
        }
    }

    public static void ExecuteExport(
        bool Expense,
        bool Income,
        int Month,
        int Year
    )
    {
        if (Expense) {
            FileHandler.Export(ChangeType.Expense, Month, Year);
            return;
        }

        if (Income) {
            FileHandler.Export(ChangeType.Income, Month, Year);
            return;
        }

        Log.Error("Provide a valid option to export.");
    }

    /// <summary>
    /// Command to list changes.
    /// <br />
    /// <paramref name="Expense" />
    /// <param name="Expense">boolean value that indicates if the changes are expenses.</param>
    /// <br />
    /// <paramref name="Income" /> 
    /// <param name="Income">boolean value that indicates if the changes are incomes.</param>
    /// <br />
    /// <paramref name="Day" /> 
    /// <param name="Day">Filter the changes by day.</param>
    /// <br />
    /// <paramref name="Month" /> 
    /// <param name="Month">Filter the changes by month.</param>
    /// <br />
    /// <paramref name="Year" /> 
    /// <param name="Year">Filter the changes by year.</param>
    /// </summary>
    public static void ExecuteList(
        bool Expense,
        bool Income,
        int Day,
        int Month,
        int Year
    ) {
        try {
            //* Validate type
            if (Expense && Income)
                throw new Exception("You can list either expenses or incomes, not both.");
            else if (!Expense && !Income)
                throw new Exception("Provide a valid type to list.");

            //* Get all
            AppDbContext context = new AppDbContext();
            IQueryable<ChangeBase> changes = Expense ? context.Expenses : context.Incomes;

            //* Filter by day
            if (GenericController.DayIsValid(Day))
                changes = changes.ByDay(Day);

            //* Filter by month
            if (GenericController.MonthIsValid(Month))
                changes = changes.ByMonth(Month);

            //* Filter by year
            if (GenericController.YearIsValid(Year))
                changes = changes.ByYear(Year);

            //* Print if none
            if (changes.Count() == 0) {
                String type = Expense ? "expenses" : "incomes";
                Log.Warning($"There are no {type} to list.");
                return;
            }

            //* Sort & show result
            changes = changes.OrderBy(c => c.Id);
            foreach (ChangeBase change in changes)
                Log.Information(change.ToString("list"));
        } catch (Exception e) {
            Log.Error(e.Message);
            // Log.Error(e.StackTrace); //? Debug
        }
    }

    /// <summary>
    /// Command to remove a change.
    /// <br />
    /// <paramref name="Expense" />
    /// <param name="Expense">boolean value that indicates if the change is expense.</param>
    /// <br />
    /// <paramref name="Income" /> 
    /// <param name="Income">boolean value that indicates if the change is income.</param>
    /// <br />
    /// <paramref name="Id" /> 
    /// <param name="Id">is the ID of the change to remove.</param>
    /// </summary>
    public static void ExecuteRemove(
        bool Expense,
        bool Income,
        int Id
    ) {
        try {
            //* Validate id
            if (Id == 0)
                throw new Exception("Provide a valid ID.");

            //* Validate type
            if (Expense && Income)
                throw new Exception("You can remove either an expense or an income, not both.");
            else if (!Expense && !Income)
                throw new Exception("Provide a valid type to remove.");
            String type = Expense ? "Expense" : "Income";

            //* Remove change
            if (Expense)
                MoneyHandler.RemoveChange<Expense>(Id);
            else
                MoneyHandler.RemoveChange<Income>(Id);

            Log.Information($"{type} removed successfully.");
        } catch (Exception e) {
            Log.Error(e.Message);
            // Log.Error(e.StackTrace); //? Debug
        }
    }
}