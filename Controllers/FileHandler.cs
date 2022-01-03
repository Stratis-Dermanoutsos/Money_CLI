namespace Money_CLI.Controllers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Money_CLI.Models;
using Money_CLI.Models.Enums;

public class FileHandler : Handler
{
    private static readonly string rootFolder = @"\Volumes\Stratis_SSD\PERSONAL\Money\";

    /// <summary>
    /// Get the string file name for the current date.
    /// </summary>
    public static string CurrentFileName()
    {
        // Get necessary date information
        int[] date = GetDate();
        string monthFullName = DateTime.Now.ToString("MMMM");

        return $"{date[1].ToString("00")}-{monthFullName}";
    }

    /// <summary>
    /// Return the full path to the current file.
    /// If it does not exist, create it.
    /// </summary>
    public static string? GetFile(ChangeType changeType)
    {
        try {
            // Get necessary date information
            int[] date = GetDate();

            // Set the category folder
            string categoryFolder;
            switch (changeType) {
                case ChangeType.Income:
                    categoryFolder = "Income";
                    break;
                default:
                    categoryFolder = "Expenses";
                    break;
            }

            // Create the folder, if it doesn't exist
            string folderPath = @$"{rootFolder}{categoryFolder}/{date[2]}/".Replace("\\", "/");
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Created folder: " + folderPath);
            }

            // Create the file, if it doesn't exist
            string fullPath = @$"{folderPath}{CurrentFileName()}.md";
            if (!File.Exists(fullPath)) {
                File.Create(fullPath).Close();
                Console.WriteLine("Created file: " + fullPath);
            }

            Console.WriteLine("Your " + (changeType == ChangeType.Expense ? "expenses" : "income") + $" will be exported to: '{fullPath}'");

            return fullPath;
        } catch (Exception) {
            return null;
        }
    }

    /// <summary>
    /// Export all changes to the current file.
    /// </summary>
    public static bool Export(ChangeType changeType)
    {
        string? file = GetFile(changeType);

        if (file == null)
            return false;

        try {
            int month = GetDate()[1];
            int year = GetDate()[2];
            List<string> template;
            double total = 0;

            switch (changeType)
            {
                case ChangeType.Income:
                    // Get total amount
                    total = MoneyHandler.TotalMonthlyIncome(month, year);

                    // Load the file template
                    template = FileTemplates.FileTemplate(
                            "Income",
                            CurrentFileName(),
                            total
                        ).ToList();

                    // Get all entries and add them to the list
                    foreach (Income income in MoneyHandler.AllMonthlyIncome(month, year))
                        template.Add(income.ToString());

                    break;
                case ChangeType.Expense:
                    // Get total amount
                    total = MoneyHandler.TotalMonthlyExpenses(month, year);

                    // Load the file template
                    template = FileTemplates.FileTemplate(
                            "Expenses",
                            CurrentFileName(),
                            total
                        ).ToList();

                    // Get all entries and add them to the list
                    foreach (Expense expense in MoneyHandler.AllMonthlyExpenses(month, year))
                        template.Add(expense.ToString());

                    break;
                default:
                    return false;
            }

            // Write the file
            File.WriteAllLines(file, template);

            return true;
        } catch (Exception) {
            return false;
        }
    }
}
