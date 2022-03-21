namespace Money_CLI;

using Serilog;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using System.IO;
using Money_CLI.CommandLine;
using Money_CLI.Controllers;
using Money_CLI.Models.Enums;

class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static async Task<int> Main(params string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        // First, set the SystemVariables, in case it does not exist.
        SystemVariables.EnsureCreated();

        // Then, ensure that there is a database to work with.
        using (AppDbContext context = new AppDbContext()) {
            context.Database.EnsureCreated();
        }

        return await Commands.Root.InvokeAsync(args);
    }
}