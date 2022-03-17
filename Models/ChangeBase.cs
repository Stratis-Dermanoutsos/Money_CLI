namespace Money_CLI.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

public class ChangeBase
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "New Change";

    [Required]
    public double Amount { get; set; } = 0;

    public int Day { get; set; } = DateTime.Today.Day;

    public int Month { get; set; } = DateTime.Today.Month;

    public int Year { get; set; } = DateTime.Today.Year;

    public string Comment { get; set; } = string.Empty;

    [NotMapped]
    public DateOnly Date {
        get => new DateOnly(Year, Month, Day);
    }

    #region ToString overrides
    /// <summary>
    /// Returns a string representation of the object.
    /// Format: | Title | Amount € | Date | Comment |
    /// </summary>
    public override string ToString()
    {
        return $"| {Title} | {Amount.ToString("C", new CultureInfo(SystemVariables.Currency))} | {Date} | {Comment} |";
    }

    /// <summary>
    /// Returns a string representation of the object.
    /// </summary>
    public string ToString(string format)
    {
        if (format != "table")
            return $"{Id}:\t{Date} - {Title} - {Amount.ToString("C", new CultureInfo(SystemVariables.Currency))}";

        return this.ToString();
    }
    #endregion

    #region Building methods
    public ChangeBase SetTitle(string Title)
    {
        this.Title = Title;
        return this;
    }

    public ChangeBase SetAmount(double Amount)
    {
        this.Amount = Amount;
        return this;
    }

    public ChangeBase SetDay(int Day)
    {
        this.Day = Day;
        return this;
    }

    public ChangeBase SetMonth(int Month)
    {
        this.Month = Month;
        return this;
    }

    public ChangeBase SetYear(int Year)
    {
        this.Year = Year;
        return this;
    }

    public ChangeBase SetComment(string Comment)
    {
        this.Comment = Comment;
        return this;
    }
    #endregion
}
