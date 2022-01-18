namespace Money_CLI.CommandLine;

using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;

public class Options
{
    #region System Variables Setters
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
    #endregion

    #region Change Type
    public static Option Expense = new Option(
        aliases: new string[] { "--expense", "-e" },
        description: "Tell the command to handle expenses."
    );

    public static Option Income = new Option(
        aliases: new string[] { "--income", "-i" },
        description: "Tell the command to handle income."
    );
    #endregion

    #region Change Properties
    public static Option<string> Title = new Option<string>(
        aliases: new string[] { "--title" },
        description: "Set custom title for the expense/income."
    );

    public static Option<double> Amount = new Option<double>(
        aliases: new string[] { "--amount" },
        description: "Set custom amount for the expense/income."
    );

    public static Option<int> Year = new Option<int>(
        aliases: new string[] { "--year" },
        description: "Set year for the expense/income."
    );

    public static Option<int> Month = new Option<int>(
        aliases: new string[] { "--month" },
        description: "Set month for the expense/income."
    );

    public static Option<int> Day = new Option<int>(
        aliases: new string[] { "--day" },
        description: "Set day for the expense/income."
    );

    public static Option<string> Comment = new Option<string>(
        aliases: new string[] { "--comment" },
        description: "Set custom comment for the expense/income."
    );
    #endregion
}