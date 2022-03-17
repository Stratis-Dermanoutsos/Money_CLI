namespace Money_CLI.Models;

public class Expense : ChangeBase
{
    public Expense(ChangeBase cb)
    {
        this.Title = cb.Title;
        this.Amount = cb.Amount;
        this.Year = cb.Year;
        this.Month = cb.Month;
        this.Day = cb.Day;
        this.Comment = cb.Comment;
    }

    public Expense(): base() { }
}