using FluentSQL.Models;

namespace FluentSQL
{
    public interface IQuery
    {
        /// <summary>
        /// The Query
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Columns of the query
        /// </summary>
        IEnumerable<ColumnAttribute> Columns { get; }

        /// <summary>
        /// Query criteria
        /// </summary>
        IEnumerable<CriteriaDetail>? Criteria { get; }

        /// <summary>
        /// Options to use in the query
        /// </summary>
        ConnectionOptions ConnectionOptions { get; }
    }

    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQuery<T> : IQuery where T : class, new()
    {
    }
}
