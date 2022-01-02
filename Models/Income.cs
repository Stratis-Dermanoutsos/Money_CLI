namespace Money_CLI.Models;

public class Income : ChangeBase
{
    // Constructors
    public Income(string title, double amount, int year, int month, int day, string comment)
        : base(title, amount, year, month, day, comment)
    {
    }

    public Income(string change)
        : base(change)
    {
    }
}
