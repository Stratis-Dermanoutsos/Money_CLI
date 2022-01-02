using System;
using Money_CLI;
using Money_CLI.Controllers;
using Money_CLI.Models;
using Money_CLI.Models.Enums;

class Program
{
    static void Main(string[] args)
    {
        FileHandler.Export(ChangeType.Income);
        FileHandler.Export(ChangeType.Expense);
    }
}