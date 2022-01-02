namespace Money_CLI.Controllers;

using System;
using Money_CLI.Models.Enums;

public static class ArgumentsHandler
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
        //* Use the export command.
        if (args[0] == "export") {
            if (args.Length == 1) {
                PrintError("No type provided.");
                return;
            }

            if (args.Length == 2) {
                ChangeType type;

                if (args[1].ToLower() == "income" || args[1].ToLower() == "i") {
                    type = ChangeType.Income;
                } else if (args[1].ToLower() == "expense" || args[1].ToLower() == "e") {
                    type = ChangeType.Expense;
                } else {
                    PrintError("Invalid type.");
                    return;
                }

                FileHandler.Export(type);
                return;
            }

            PrintError("Too many arguments provided.");
            return;
        }

        PrintError("Invalid argument(s).");
        #endregion
    }

    /// <summary>
    /// Prints a message to the console, in error format.
    /// </summary>
    public static void PrintError(string message)
    {
        Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Red);
    }
}