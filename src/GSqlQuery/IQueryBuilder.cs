using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder
    /// </summary>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options type</typeparam>
    public interface IQueryBuilder<TReturn, TOptions> : IBuilder<TReturn> , IOptions<TOptions>
        where TReturn : IQuery
    {
        /// <summary>
        /// Get columns
        /// </summary>
        IEnumerable<PropertyOptions> Columns { get; }
    }
}