using GSqlQuery.Cache;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder
    /// </summary>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    public interface IQueryBuilder<TReturn, TQueryOptions> : IBuilder<TReturn>, IQueryOptions<TQueryOptions>
        where TReturn : IQuery
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Get columns
        /// </summary>
        PropertyOptionsCollection Columns { get; }
    }
}