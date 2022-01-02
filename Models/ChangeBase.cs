namespace Money_CLI.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Money_CLI.Models.Enums;

public class ChangeBase
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public double Amount { get; set; }

    public int Day { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    [NotMapped]
    public DateOnly Date {
        get => new DateOnly(Year, Month, Day);
        set {
            Year = value.Year;
            Month = value.Month;
            Day = value.Day;
        }
    }

    public string Comment { get; set; }

    #region Constructors
    /// <summary>
    /// This constructor is required to create ChangeBase objects.
    /// </summary>
    public ChangeBase(
        string title,
        double amount,
        int year, int month, int day, string comment)
    {
        Title = title;
        Amount = amount;
        Year = year;
        Month = month;
        Day = day;
        Comment = comment;
    }

    /// <summary>
    /// This constructor is required to create ChangeBase objects from a string.
    /// </summary>
    public ChangeBase(string change)
    {
        string[] parts = change.Split('|', StringSplitOptions.TrimEntries);
        int[] dateParts = Array.ConvertAll(parts[3].Split('/'), int.Parse);
        Day = dateParts[0];
        Month = dateParts[1];
        Year = dateParts[2];

        Title = parts[1];
        Amount = double.Parse(parts[2]);
        Comment = parts[4];
    }

    /// <summary>
    /// Constructor to return another object of the same type.
    /// </summary>
    public ChangeBase(ChangeType changeType)
    {

        Amount = 0;
        DateOnly date = new DateOnly();
        Year = date.Year;
        Month = date.Month;
        Day = date.Day;

        Comment = string.Empty;

        switch (changeType)
        {
            case ChangeType.Income:
                Title = "New Income";
                new Income(Title, Amount, Year, Month, Day, Comment);
                break;
            default:
                Title = "New Expense";
                new Expense(Title, Amount, Year, Month, Day, Comment);
                break;
        }
    }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public ChangeBase()
    {
        Title = string.Empty;
        Amount = 0;
        DateOnly date = new DateOnly();
        Year = date.Year;
        Month = date.Month;
        Day = date.Day;
        Comment = string.Empty;
    }
    #endregion

    /// <summary>
    /// Returns a string representation of the object.
    /// Format: | Title | Amount € | Date | Comment |
    /// </summary>
    public override string ToString()
    {
        return $"| {Title} | {Amount.ToString("C", new CultureInfo("el-GR"))} | {Date} | {Comment} |";
    }
}
