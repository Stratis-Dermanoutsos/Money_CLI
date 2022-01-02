namespace Money_CLI.Models;

public class Expense : ChangeBase
{
    // Constructors
    public Expense(string title, double amount, int year, int month, int day, string comment)
        : base(title, amount, year, month, day, comment)
    {
    }

    public Expense(string change)
        : base(change)
    {
    }
}