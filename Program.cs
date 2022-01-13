namespace Money_CLI;

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
        // First, set the SystemVariables, in case it does not exist.
        SystemVariables.Create();

        return await Commands.Root.InvokeAsync(args);
    }
}