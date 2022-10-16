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

    #region Day, Month and Year validations
    /// <summary>
    /// Validates the value of the day provided.
    /// <br />
    /// <paramref name="day"/>
    /// <param name="day">Day to be validated</param>
    /// <br />
    /// <returns>Returns true if the day is valid, false otherwise.</returns>
    /// </summary>
    public static bool DayIsValid(int day)
    {
        return 1 <= day && day <= 31;
    }

    /// <summary>
    /// Validates the value of the month provided.
    /// <br />
    /// <paramref name="month"/>
    /// <param name="month">Month to be validated</param>
    /// <br />
    /// <returns>Returns true if the month is valid, false otherwise.</returns>
    /// </summary>
    public static bool MonthIsValid(int month)
    {
        return 1 <= month && month <= 12;
    }

    /// <summary>
    /// Validates the value of the year provided.
    /// <br />
    /// <paramref name="year"/>
    /// <param name="year">Year to be validated</param>
    /// <br />
    /// <returns>Returns true if the year is valid, false otherwise.</returns>
    /// </summary>
    public static bool YearIsValid(int year)
    {
        return 1 <= year && year <= DateTime.Now.Year;
    }
    #endregion
}