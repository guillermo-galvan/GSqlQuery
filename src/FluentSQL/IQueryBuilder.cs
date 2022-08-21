using FluentSQL.Models;

namespace FluentSQL
{
    /// <summary>
    /// Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQueryBuilder<T, TReturn> where T : class, new() where  TReturn : IQuery<T>
    {
        /// <summary>
        /// Statements to use in the query
        /// </summary>
        ConnectionOptions ConnectionOptions { get; }

        /// <summary>
        /// Build Query
        /// </summary>
        TReturn Build();
    }
}
