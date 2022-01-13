namespace Money_CLI.Controllers;

using System;
using Money_CLI.Models;
using Money_CLI.Models.Enums;

public class ArgumentsHandler : Handler
{
    /// <summary>
    /// Gets the arguments from the command line.
    /// Then, based on the input, call the appropriate method.
    /// </summary>
    public static void HandleArgs(string[] args)
    {
        #region options
        //* Show the help message.
        if (args.Length == 0 || args[0] == "--help" || args[0] == "-h") {
            Console.WriteLine(
                Environment.NewLine +
                "Usage: money [options] [command] [command options]" + Environment.NewLine +
                Environment.NewLine +
                "Options:" + Environment.NewLine +
                "  -h|--help\t\t\tDisplay help." + Environment.NewLine +
                "  --set-folder <path>\t\tSet root folder for the export function." + Environment.NewLine + // TODO
                "  --set-database <path>\t\tSet database folder path." + Environment.NewLine + // TODO
                "  --set-currency <code>\t\tSet the currency for the exported file." + Environment.NewLine + // TODO
                Environment.NewLine +
                "Commands:" + Environment.NewLine +
                "  add <type> [arguments]\tAdd expense/income." + Environment.NewLine + // TODO
                "  export <type>\t\t\tExport expenses/incomes." + Environment.NewLine +
                "  list <type>\t\t\tList expenses/incomes." + Environment.NewLine + // TODO
                "  remove <type> <id>\t\tRemove expense/income." + Environment.NewLine + // TODO
                Environment.NewLine +
                "Command options:" + Environment.NewLine +
                "  -h|--help\t\t\tDisplay help about the specified command." + Environment.NewLine +
                Environment.NewLine +
                "Types:" + Environment.NewLine +
                "  e|expense\tExpense." + Environment.NewLine +
                "  i|income\tIncome." + Environment.NewLine +
                Environment.NewLine
            );
            return;
        }
        #endregion

        #region commands
        //* The export command.
        if (args[0] == "export") {
            if (args.Length == 1) {
                PrintError("No type provided.");
                return;
            }

            if (args.Length == 2) {
                // Get help on the command.
                if (args[1] == "--help" || args[1] == "-h") {
                    Console.WriteLine(
                        "Export the expenses/incomes." + Environment.NewLine +
                        Environment.NewLine +
                        "Usage: money export <type>" + Environment.NewLine +
                        Environment.NewLine +
                        "Types:" + Environment.NewLine +
                        "  e|expense\tExport expenses." + Environment.NewLine +
                        "  i|income\tExport incomes." + Environment.NewLine +
                        Environment.NewLine
                    );
                    return;
                }

                // Use the command.
                if (args[1] == "income" || args[1] == "i") {
                    if (!FileHandler.Export(ChangeType.Income))
                        PrintError("Could not export the income.");
                } else if (args[1] == "expense" || args[1] == "e") {
                    if (!FileHandler.Export(ChangeType.Expense))
                        PrintError("Could not export the expenses.");
                } else {
                    PrintError("Invalid type.");
                }

                return;
            }

            PrintError("Too many arguments provided.");
            return;
        }

        PrintError("Invalid argument(s).");
        #endregion
    }

}