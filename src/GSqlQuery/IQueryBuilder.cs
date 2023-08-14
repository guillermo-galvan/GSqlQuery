using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQueryBuilder<TReturn, TOptions> : IBuilder<TReturn> where TReturn : IQuery
    {
        IEnumerable<PropertyOptions> Columns { get; }

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        TOptions Options { get; }
    }
}