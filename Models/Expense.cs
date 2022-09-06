namespace Money_CLI.Models;

public class Expense : ChangeBase
{
    public Expense(ChangeBase cb)
    {
        this.Date_in = cb.Date_in;
        this.Title = cb.Title;
        this.Amount = cb.Amount;
        this.Year = cb.Year;
        this.Month = cb.Month;
        this.Day = cb.Day;
        this.Comment = cb.Comment;
    }

    public Expense(): base() { }
}