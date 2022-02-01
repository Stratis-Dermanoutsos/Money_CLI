namespace Money_CLI.Controllers;

using System;
using System.IO;

// TODO: Change the name to anything more meaningful.

public class GenericController
{
    /// <summary>
    /// Returns the date in array format.
    /// </summary>
    public static int[] GetDate
    {
        get => new int[] {
            DateTime.Now.Day,
            DateTime.Now.Month,
            DateTime.Now.Year
        };
    }

    #region Logging
    /// <summary>
    /// Prints a message to the console, in default format.
    /// </summary>
    public static void PrintDefault(string message)
    {
        Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.White);
    }

    /// <summary>
    /// Prints a message to the console, in error format.
    /// </summary>
    public static void PrintError(string message)
    {
        Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Red);
    }

    /// <summary>
    /// Prints a message to the console, in success format.
    /// </summary>
    public static void PrintSuccess(string message)
    {
        Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Green);
    }

    /// <summary>
    /// Prints a message to the console, in warning format.
    /// </summary>
    public static void PrintWarning(string message)
    {
        Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Yellow);
    }
    #endregion

    /// <summary>
    /// Ensures a directory contains correct Separator characters.
    /// For better understanding, go to https://docs.microsoft.com/en-us/dotnet/api/system.io.path.directoryseparatorchar
    /// </summary>
    public static string EnsureDirectory(string directory)
    {
        string sepChar = Path.DirectorySeparatorChar.ToString();
        string altChar = Path.AltDirectorySeparatorChar.ToString();

        if (!directory.EndsWith(sepChar) && !directory.EndsWith(altChar))
            directory += sepChar;

        if (OperatingSystem.IsWindows())
            return directory.Replace(altChar, sepChar);

        return directory.Replace("\\", sepChar);
    }

    #region Month and Year validations
    /// <summary>
    /// Validates the value of the month from the input.
    /// </summary>
    public static bool MonthIsValid(int month)
    {
        return 1 <= month && month <= 12;
    }

    /// <summary>
    /// Validates the value of the year from the input.
    /// </summary>
    public static bool YearIsValid(int year)
    {
        return 1 <= year && year <= DateTime.Now.Year;
    }
    #endregion
}