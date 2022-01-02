namespace Money_CLI;

using Money_CLI.Controllers;

class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// It does not actually do anything other than pass the arguments to the handler.
    /// </summary>
    static void Main(string[] args)
    {
        ArgumentsHandler.HandleArgs(args);
    }
}