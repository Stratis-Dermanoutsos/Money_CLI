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