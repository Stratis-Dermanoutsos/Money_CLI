namespace Money_CLI.Controllers;

// TODO: Change the name to anything more meaningful.

public class GenericController
{
    /// <summary>
    /// Returns the date in array format.
    /// </summary>
    public static int[] GetDate()
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;

        return new int[] { day, month, year };
    }

    /// <summary>
    /// Prints a message to the console, in error format.
    /// </summary>
    public static void PrintError(string message)
    {
        Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Red);
    }
}