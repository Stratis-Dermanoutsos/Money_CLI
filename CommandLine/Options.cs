namespace Money_CLI.CommandLine;

using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;

public class Options
{
    public static Option<string> SetExport = new Option<string>(
        aliases: new string[] { "--set-export" },
        description: "Set export folder path."
    );

    public static Option<string> SetDatabase = new Option<string>(
        aliases: new string[] { "--set-database" },
        description: "Set database folder path."
    );

    public static Option<string> SetCurrency = new Option<string>(
        aliases: new string[] { "--set-currency" },
        description: "Set the currency to export."
    );

    public static Option Expense = new Option(
        aliases: new string[] { "--expense", "-e" },
        description: "Tell the command to handle expenses."
    );

    public static Option Income = new Option(
        aliases: new string[] { "--income", "-i" },
        description: "Tell the command to handle income."
    );
}