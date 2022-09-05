namespace Money_CLI.Controllers;

using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Money_CLI.Models;
using Money_CLI.Models.Enums;

public class FileHandler : GenericController
{
    #region FileName based on month
    /// <summary>
    /// Returns the string file name for the current date.
    /// </summary>
    public static string CurrentFileName
    {
        get
        {
            string monthFullName = DateTime.Now.ToString("MMMM");

            return $"{GetDate[1].ToString("00")}-{monthFullName}";
        }
    }

    /// <summary>
    /// Returns the string file name for the selected month.
    /// </summary>
    public static string FileName(int month)
    {
        string monthFullName = DateTime.Parse($"01/{month}/1970").ToString("MMMM"); // TODO

        return $"{month.ToString("00")}-{monthFullName}";
    }
    #endregion

    /// <summary>
    /// Return the full path to the current file.
    /// If it does not exist, create it.
    /// </summary>
    public static string? GetFile(ChangeType changeType, int month, int year)
    {
        try
        {
            string categoryFolder;
            string folderPath = SystemVariables.ExportFolder; // default value only set to prevent null returns
            string filename = "ExportedOn" + DateTime.Now.ToString("yyyyMMdd-HHmm"); // default value only set to prevent null returns

            switch (changeType)
            {
                case ChangeType.Income:
                    categoryFolder = "Income";
                    break;
                case ChangeType.Expense:
                    categoryFolder = "Expense";
                    break;
                default:
                    Log.Error("ChangeType enum could not be determined for file export naming.");
                    return null;
            }

            // If specific year and month are specified
            if (MonthIsValid(month) && YearIsValid(year))
            {
                folderPath = EnsureDirectory(@$"{SystemVariables.ExportFolder}{categoryFolder}/{year}/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Log.Information("Created folder {folderPath}", folderPath);
                }

                filename = FileName(month);
            }

            // If only specific year is specified
            else if (!MonthIsValid(month) && YearIsValid(year))
            {
                folderPath = EnsureDirectory(@$"{SystemVariables.ExportFolder}{categoryFolder}/{year}/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Log.Information("Created folder {folderPath}", folderPath);
                }
            }

            // If only specific month is specified
            else if (MonthIsValid(month) && !YearIsValid(year))
            {
                folderPath = EnsureDirectory(@$"{SystemVariables.ExportFolder}{categoryFolder}/{FileName(month)}/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Log.Information("Created folder {folderPath}", folderPath);
                }
            }

            // If neither month or year are specified, export all table data to the root categoryFolder
            else
            {
                folderPath = EnsureDirectory(@$"{SystemVariables.ExportFolder}{categoryFolder}/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Log.Information("Created folder {folderPath}", folderPath);
                }
            }

            // Create the file, if it doesn't exist
            string fullPath = @$"{folderPath}{filename}.md";

            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Close();
                Log.Information("Created file {fullPath}", fullPath);
            }

            Log.Information("Your {changeType} will be exported to {fullPath}", (changeType == ChangeType.Expense ? "expenses" : "income"), fullPath);

            return fullPath;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Exports all changes to the current file.
    /// </summary>
    /// 
    public static void Export(ChangeType changeType, int month, int year)
    {
        string? file;
        List<string> outputFileContents = new List<string>();
        List<string> outputFileContentsItems = new List<string>();
        double total = 0;

        if (changeType == ChangeType.Expense)
        {
            try
            {
                List<Expense> expenses;

                // Only get the expenses that are specified by the user, or all.
                if (MonthIsValid(month) && YearIsValid(year))
                    expenses = MoneyHandler.AllMonthlyExpensesById(month, year);

                else if (MonthIsValid(month) && !YearIsValid(year))
                    expenses = MoneyHandler.AllExpensesOnMonth(month);

                else if (!MonthIsValid(month) && YearIsValid(year))
                    expenses = MoneyHandler.AllExpensesOnYear(year);

                else
                    expenses = MoneyHandler.AllExpenses();

                if (expenses.Count == 0)
                {
                    Log.Warning("There are no expenses to list.");
                    return;
                }

                // Get all entries and add them to the list
                foreach (Expense expense in expenses)
                {
                    outputFileContentsItems.Add(expense.ToString());
                    total += expense.Amount; //TODO: this line makes MoneyHandler.TotalMonthlyExpenses and MoneyHandler.TotalMonthlyIncome redundant (remove functions?)
                }

                outputFileContents = FileTemplates.FileTemplate(
                        changeType.ToString(),
                        MonthIsValid(month) ? FileName(month) : CurrentFileName,
                        total
                    ).ToList();

                outputFileContents = outputFileContents.Concat(outputFileContentsItems).ToList(); // Combine lists and place the header at the beginning

                file = GetFile(changeType, month, year); // Generate file name here so that paths are not being generated for empty result table queries
                if (file == null)
                {
                    Log.Error("Export could not be completed due to file naming error.");
                    return;
                }

                // Write the file
                File.WriteAllLines(file, outputFileContents);
                return;
            }
            catch (Exception)
            {
                Log.Error("Error when trying to export expenses to file.");
                return;
            }
        }

        if (changeType == ChangeType.Income)
        {
            try
            {
                List<Income> incomes;

                // Only get the income that is specified by the user, or all.
                if (MonthIsValid(month) && YearIsValid(year))
                    incomes = MoneyHandler.AllMonthlyIncomeById(month, year);

                else if (MonthIsValid(month) && !YearIsValid(year))
                    incomes = MoneyHandler.AllIncomeOnMonth(month);

                else if (!MonthIsValid(month) && YearIsValid(year))
                    incomes = MoneyHandler.AllIncomeOnYear(year);

                else
                    incomes = MoneyHandler.AllIncome();

                if (incomes.Count == 0)
                {
                    Log.Warning("There are no incomes to list.");
                    return;
                }

                // Get all entries and add them to the list
                foreach (Income income in incomes)
                {
                    outputFileContentsItems.Add(income.ToString());
                    total += income.Amount; //TODO: this line makes MoneyHandler.TotalMonthlyExpenses and MoneyHandler.TotalMonthlyIncome redundant (remove functions?)
                }

                outputFileContents = FileTemplates.FileTemplate(
                        changeType.ToString(),
                        MonthIsValid(month) ? FileName(month) : CurrentFileName,
                        total
                    ).ToList();

                outputFileContents = outputFileContents.Concat(outputFileContentsItems).ToList(); // Combine lists and place the header at the beginning

                file = GetFile(changeType, month, year); // Generate file name here so that paths are not being generated for empty result table queries
                if (file == null)
                {
                    Log.Error("Export could not be completed due to file naming error.");
                    return;
                }

                // Write the file
                File.WriteAllLines(file, outputFileContents);
                return;
            }
            catch (Exception)
            {
                Log.Error("Error when trying to export expenses to file.");
                return;
            }
        }
    }
}





