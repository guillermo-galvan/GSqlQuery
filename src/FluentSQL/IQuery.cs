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
    }

    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQuery<T> : IQuery where T : class, new()
    {
        /// <summary>
        /// Options to use in the query
        /// </summary>
        IStatements Statements { get; }
    }

    public interface IQuery<T, TDbConnection, TResult> : IQuery, IExecute<TResult,TDbConnection> where T : class, new()
    {
        ConnectionOptions<TDbConnection> ConnectionOptions { get; }
    }
}
