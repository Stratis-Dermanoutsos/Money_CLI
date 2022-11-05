namespace Money_CLI.Controllers;

using Money_CLI.Models;

public static class QueryShortener
{
    /// <summary>
    /// <paramref name="entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name="day" />
    /// <param name="day">The day to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by day.</returns>
    /// </summary>
    public static IQueryable<T> ByDay<T>(this IQueryable<T> entities, int day) where T : ChangeBase
    {
        return entities.Where(i => i.Day == day);
    }

    /// <summary>
    /// <paramref name="entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name="month" />
    /// <param name="month">The month to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by month.</returns>
    /// </summary>
    public static IQueryable<T> ByMonth<T>(this IQueryable<T> entities, int month) where T : ChangeBase
    {
        return entities.Where(i => i.Month == month);
    }

    /// <summary>
    /// <paramref name="entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name="year" />
    /// <param name="year">The year to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by year.</returns>
    /// </summary>
    public static IQueryable<T> ByYear<T>(this IQueryable<T> entities, int year) where T : ChangeBase
    {
        return entities.Where(i => i.Year == year);
    }

    /// <summary>
    /// <paramref name="entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name="title" />
    /// <param name="title">The title to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by title.</returns>
    /// </summary>
    public static IQueryable<T> ByTitle<T>(this IQueryable<T> entities, string title) where T : ChangeBase
    {
        return entities.Where(i => i.Title.Contains(title));
    }

    /// <summary>
    /// <paramref name="entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name="comment" />
    /// <param name="comment">The comment to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by comment.</returns>
    /// </summary>
    public static IQueryable<T> ByComment<T>(this IQueryable<T> entities, string comment) where T : ChangeBase
    {
        return entities.Where(i => i.Comment.Contains(comment));
    }

    /// <summary>
    /// <paramref name="entities" />
    /// <param name="entities">The entities to query</param>
    /// <br />
    /// <paramref name="content" />
    /// <param name="content">The content to filter by</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes filtered by content in either title or comment.</returns>
    /// </summary>
    public static IQueryable<T> ByContent<T>(this IQueryable<T> entities, string content) where T : ChangeBase
    {
        return entities.Where(i => i.Title.Contains(content) || i.Comment.Contains(content));
    }

    /// <summary>
    /// <paramref name="entities" />
    /// <param name="entities">The entities to order</param>
    /// <br />
    /// <returns>Returns an IQueryable containing changes ordered by id.</returns>
    /// </summary>
    public static IQueryable<T> Ordered<T>(this IQueryable<T> entities) where T : ChangeBase
    {
        return entities.OrderBy(i => i.Id);
    }
}