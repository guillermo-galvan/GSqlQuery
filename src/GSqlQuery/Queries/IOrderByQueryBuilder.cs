using System;

namespace GSqlQuery
{
    /// <summary>
    /// Order By Query Builder
    /// </summary>
    public interface IOrderByQueryBuilder<T, TReturn, TQueryOptions> : IBuilder<TReturn>, IQueryOptions<TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Add Columns
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        void AddOrderBy<TProperties>(Func<T, TProperties> func, OrderBy orderBy);
    }
}