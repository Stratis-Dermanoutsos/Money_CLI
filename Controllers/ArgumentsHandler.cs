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
        //* Show the help message.
        if (args.Length == 0 || args[0] == "--help" || args[0] == "-h") {
            Console.WriteLine(
                "Thank you for using Money_CLI!" + Environment.NewLine +
                Environment.NewLine +
                "Usage: money [options]" + Environment.NewLine +
                Environment.NewLine +
                "Options:" + Environment.NewLine +
                "  -h|--help\t\t\t\t\tDisplay help." + Environment.NewLine +
                "  -a|--add <type> [additional arguments]\tAdd expense/income." + Environment.NewLine + // TODO
                "  -e|--export <type>\t\t\t\tExport expenses/incomes." + Environment.NewLine +
                "  -l|--list <type>\t\t\t\tList expenses/incomes." + Environment.NewLine + // TODO
                "  -r|--remove <type> <id>\t\t\tRemove expense/income." + Environment.NewLine + // TODO
                "  --set-folder <type> <path>\t\t\tSet root folder for the export function." + Environment.NewLine + // TODO
                "  --set-database <path>\t\t\t\tSet database path." + Environment.NewLine + // TODO
                Environment.NewLine +
                "Types:" + Environment.NewLine +
                "  e|expense\tExpense." + Environment.NewLine +
                "  i|income\tIncome." + Environment.NewLine
            );
            return;
        }

        //* Use the export functionality.
        if (args[0] == "--export" || args[0] == "-e")
        {
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
    }

    /// <summary>
    /// Prints a message to the console, in error format.
    /// </summary>
    public static void PrintError(string message)
    {
        Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Red);
    }
}