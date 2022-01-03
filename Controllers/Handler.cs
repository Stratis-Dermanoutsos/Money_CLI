namespace Money_CLI.Controllers;

public class Handler
{
    /// <summary>
    /// Get date in array format.
    /// </summary>
    public static int[] GetDate()
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;

        return new int[] { day, month, year };
    }
}