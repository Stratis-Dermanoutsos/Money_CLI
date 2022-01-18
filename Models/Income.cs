namespace Money_CLI.Models;

public class Income : ChangeBase
{
    // Constructors
    public Income(string title, double amount, int year, int month, int day, string comment)
        : base(title, amount, year, month, day, comment)
    {
    }

    public Income(ChangeBase cb)
    {
        this.Title = cb.Title;
        this.Amount = cb.Amount;
        this.Year = cb.Year;
        this.Month = cb.Month;
        this.Day = cb.Day;
        this.Comment = cb.Comment;
    }

    public Income(string change)
        : base(change)
    {
    }

    public Income()
        : base()
    {
    }
}
