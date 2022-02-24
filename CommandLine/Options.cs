namespace Money_CLI.CommandLine;

using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;

public class Options
{
    #region System Variables Setters
    /// <summary>
    /// Sets the directory in which expenses/income will be exported.
    /// </summary>
    public static Option<string> SetExport = new Option<string>(
        aliases: new string[] { "--set-export" },
        description: "Set export folder path."
    );

    /// <summary>
    /// Sets the folder to contain the database file.
    /// </summary>
    public static Option<string> SetDatabase = new Option<string>(
        aliases: new string[] { "--set-database" },
        description: "Set database folder path."
    );

    /// <summary>
    /// Sets the currency to be used in the exported files.
    /// </summary>
    public static Option<string> SetCurrency = new Option<string>(
        aliases: new string[] { "--set-currency" },
        description: "Set the currency to export.\n" +
                    "Supported currencies: EUR, GBP, JPY, USD."
    );
    #endregion

    #region Change Type
    /// <summary>
    /// Tells the command to handle expenses.
    /// </summary>
    public static Option Expense = new Option(
        aliases: new string[] { "--expense", "-e" },
        description: "Tell the command to handle expenses."
    );

    /// <summary>
    /// Tells the command to handle income.
    /// </summary>
    public static Option Income = new Option(
        aliases: new string[] { "--income", "-i" },
        description: "Tell the command to handle income."
    );
    #endregion

    #region Change Properties
    /// <summary>
    /// Sets the title of the expense/income.
    /// </summary>
    public static Option<string> Title = new Option<string>(
        aliases: new string[] { "--title" },
        description: "Set custom title for the expense/income."
    );

    /// <summary>
    /// Sets the amount of currency involved for the expense/income.
    /// </summary>
    public static Option<double> Amount = new Option<double>(
        aliases: new string[] { "--amount" },
        description: "Set custom amount for the expense/income."
    );

    /// <summary>
    /// Sets the year of the expense/income.
    /// </summary>
    public static Option<int> Year = new Option<int>(
        aliases: new string[] { "--year" },
        description: "Set year for the expense/income."
    );

    /// <summary>
    /// Sets the month of the expense/income.
    /// </summary>
    public static Option<int> Month = new Option<int>(
        aliases: new string[] { "--month" },
        description: "Set month for the expense/income."
    );

    /// <summary>
    /// Sets the day of the expense/income.
    /// </summary>
    public static Option<int> Day = new Option<int>(
        aliases: new string[] { "--day" },
        description: "Set day for the expense/income."
    );

    /// <summary>
    /// Sets a comment of the expense/income.
    /// </summary>
    public static Option<string> Comment = new Option<string>(
        aliases: new string[] { "--comment" },
        description: "Set custom comment for the expense/income."
    );
    #endregion

    #region Search options
    /// <summary>
    /// Sets the item Id for the item to search for.
    /// </summary>
    public static Option<int> Id = new Option<int>(
        aliases: new string[] { "--id" },
        description: "Set the id of the item to delete. Can be acquired by using the list command."
    );
    #endregion
}