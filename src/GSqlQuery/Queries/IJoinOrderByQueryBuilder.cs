using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public interface IJoinOrderByQueryBuilder<T, TReturn, TQueryOptions> : IBuilder<TReturn>, IQueryOptions<TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Add Columns
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        void AddOrderBy<TProperties>(Expression<Func<T, TProperties>> expression, OrderBy orderBy);
    }
}