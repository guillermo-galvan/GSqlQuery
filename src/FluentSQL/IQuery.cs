using FluentSQL.Models;

namespace FluentSQL
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQuery<T> where T : class, new()
    {
        /// <summary>
        /// Columns of the query
        /// </summary>
        IEnumerable<ColumnAttribute> Columns { get; }

        /// <summary>
        /// Query criteria
        /// </summary>
        IEnumerable<CriteriaDetail> Criteria { get; }

        /// <summary>
        /// Options to use in the query
        /// </summary>
        ConnectionOptions ConnectionOptions { get; }

        /// <summary>
        /// The Query
        /// </summary>
        string Text { get; }
    }
}
