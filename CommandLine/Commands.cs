namespace Money_CLI.CommandLine;

using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

public class Commands
{
    /// <summary>
    /// The root command.
    /// It is responsible for calling all the other commands and handling all arguments.
    /// </summary>
    public static RootCommand Root
    {
        get
        {
            RootCommand _root = new RootCommand(
                description: "Open source CLI tool for one to note their money usage."
            );

            _root.Handler = CommandHandler.Create<string, string, string>(Handlers.ExecuteRoot);

            // Add the commands
            _root.AddCommand(Add);
            _root.AddCommand(Export);
            _root.AddCommand(List);
            _root.AddCommand(Remove);

            // Add our options
            _root.AddOption(Options.SetExport);
            _root.AddOption(Options.SetDatabase);
            _root.AddOption(Options.SetCurrency);

            return _root;
        }
    }

    /// <summary>
    /// Adds a new expense/income.
    /// </summary>
    public static Command Add
    { 
        get
        {
            Command _add = new Command(
                "add",
                description: "Add an income or expense."
            );

            _add.Handler = CommandHandler.Create<bool, bool, string, double, int, int, int, string>(Handlers.ExecuteAdd);

            // Add our options
            _add.AddOption(Options.Expense);
            _add.AddOption(Options.Income);
            _add.AddOption(Options.Title);
            _add.AddOption(Options.Amount);
            _add.AddOption(Options.Year);
            _add.AddOption(Options.Month);
            _add.AddOption(Options.Day);
            _add.AddOption(Options.Comment);

            return _add;
        }
    }

    /// <summary>
    /// Exports income or expenses based on argument.
    /// </summary>
    public static Command Export
    {
        get
        {
            Command _export = new Command(
                "export",
                description: "Export income or expenses based on argument."
            );

            _export.Handler = CommandHandler.Create<bool, bool, int, int>(Handlers.ExecuteExport);

            _export.AddOption(Options.Expense);
            _export.AddOption(Options.Income);
            _export.AddOption(Options.Month);
            _export.AddOption(Options.Year);

            return _export;
        }
    }

    /// <summary>
    /// Lists income or expenses based on argument.
    /// </summary>
    public static Command List
    {
        get
        {
            Command _list = new Command(
                "list",
                description: "List income or expenses. Get all or filter by arguments."
            );

            _list.Handler = CommandHandler.Create<bool, bool, int, int, int>(Handlers.ExecuteList);

            _list.AddOption(Options.Expense);
            _list.AddOption(Options.Income);
            _list.AddOption(Options.Day);
            _list.AddOption(Options.Month);
            _list.AddOption(Options.Year);

            return _list;
        }
    }

    public static Command Remove
    {
        get
        {
            Command _remove = new Command(
                "remove",
                description: "Remove an income or expense."
            );

            _remove.Handler = CommandHandler.Create<bool, bool, int>(Handlers.ExecuteRemove);

            _remove.AddOption(Options.Expense);
            _remove.AddOption(Options.Income);
            _remove.AddOption(Options.Id);

            return _remove;
        }
    }
}