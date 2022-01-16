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