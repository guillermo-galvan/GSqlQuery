using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQueryBuilder<T, TReturn> : IBuilder<TReturn> where T : class, new() where  TReturn : IQuery
    {
        IEnumerable<PropertyOptions> Columns { get; }

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        IStatements Options { get; }
    }
}
