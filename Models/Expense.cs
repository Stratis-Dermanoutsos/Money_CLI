namespace Money_CLI.Models;

public class Expense : ChangeBase
{
    // Constructors
    public Expense(string title, double amount, int year, int month, int day, string comment)
        : base(title, amount, year, month, day, comment)
    {
    }

    public Expense(ChangeBase cb)
    {
        this.Title = cb.Title;
        this.Amount = cb.Amount;
        this.Year = cb.Year;
        this.Month = cb.Month;
        this.Day = cb.Day;
        this.Comment = cb.Comment;
    }

    public Expense(string change)
        : base(change)
    {
    }

    public Expense()
        : base()
    {
    }
}