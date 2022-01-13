namespace Money_CLI.CommandLine;

using Money_CLI.Controllers;
using Money_CLI.Models.Enums;

public class Handlers
{
    public static void ExecuteRoot(
        string SetExport,
        string SetDatabase,
        string SetCurrency
    ) {
        if (Directory.Exists(SetExport))
            SystemVariables.ExportFolder = SetExport;
        else
            GenericController.PrintError("Export folder does not exist.");

        // TODO: Handle SetDatabase.
        // TODO: Handle SetCurrency.
    }

    public static void ExecuteExport(
        bool Expense,
        bool Income
    ) {
        if (Expense) {
            if (!FileHandler.Export(ChangeType.Expense))
                GenericController.PrintError("Could not export the expenses.");

            return;
        }

        if (Income) {
            if (!FileHandler.Export(ChangeType.Income))
                GenericController.PrintError("Could not export the income.");

            return;
        }

        GenericController.PrintError("Provide a valid option to export.");
    }
}