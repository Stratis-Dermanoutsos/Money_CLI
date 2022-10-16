namespace Money_CLI.Controllers;

using Money_CLI.Models;

public static class QueryShortener
{
    /// <summary>
    /// <paramref name = "entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name = "day" />
    /// <param name="day">The day to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by day.</returns>
    /// </summary>
    public static IQueryable<T> ByDay<T>(this IQueryable<T> entities, int day) where T : ChangeBase
    {
        return entities.Where(i => i.Day == day);
    }

    /// <summary>
    /// <paramref name = "entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name = "month" />
    /// <param name="month">The month to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by month.</returns>
    /// </summary>
    public static IQueryable<T> ByMonth<T>(this IQueryable<T> entities, int month) where T : ChangeBase
    {
        return entities.Where(i => i.Month == month);
    }

    /// <summary>
    /// <paramref name = "entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name = "year" />
    /// <param name="year">The year to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by year.</returns>
    /// </summary>
    public static IQueryable<T> ByYear<T>(this IQueryable<T> entities, int year) where T : ChangeBase
    {
        return entities.Where(i => i.Year == year);
    }
}