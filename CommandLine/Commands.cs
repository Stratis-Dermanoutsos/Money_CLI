namespace Money_CLI.CommandLine;

using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

public class Commands
{
    public static RootCommand Root
    {
        get
        {
            RootCommand _root = new RootCommand(
                description: "Add an income or expense."
            );

            _root.Handler = CommandHandler.Create<string, string, string>(Handlers.ExecuteRoot);

            // Add the commands
            _root.AddCommand(Add);
            _root.AddCommand(Export);
            _root.AddCommand(Remove);

            // Add our options
            _root.AddOption(Options.SetExport);
            _root.AddOption(Options.SetDatabase);
            _root.AddOption(Options.SetCurrency);

            return _root;
        }
    }

    public static Command Add
    { 
        get
        {
            Command _add = new Command(
                "add",
                description: "Add an income or expense."
            );

            // _add.Handler = CommandHandler.Create<string, string, string>(Handlers.ExecuteAdd);

            _add.AddOption(Options.Expense);
            _add.AddOption(Options.Income);

            return _add;
        }
    }

    /// <summary>
    /// Export income or expenses based on argument.
    /// </summary>
    public static Command Export
    {
        get
        {
            Command _export = new Command(
                "export",
                description: "Export income or expenses based on argument."
            );

            _export.Handler = CommandHandler.Create<bool, bool>(Handlers.ExecuteExport);

            _export.AddOption(Options.Expense);
            _export.AddOption(Options.Income);

            return _export;
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

            // _remove.Handler = CommandHandler.Create<string, string, string>(Handlers.ExecuteRemove);

            _remove.AddOption(Options.Expense);
            _remove.AddOption(Options.Income);

            return _remove;
        }
    }
}