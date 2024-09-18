using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Select Query Builder Extension
    /// </summary>
    public static class SelectQueryBuilderExtension
    {
        /// <summary>
        /// Generate the count query.
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a select query</param>
        /// <returns>IQueryBuilderWithWhere&lt;<typeparamref name="T"/>,CountQuery&lt;<typeparamref name="T"/>&gt;&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilderWithWhere<T, CountQuery<T>, QueryOptions> Count<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>, QueryOptions> queryBuilder)
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }
            return new CountQueryBuilder<T>(queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a select query</param>
        /// <param name="func">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IOrderByQueryBuilder<T, OrderByQuery<T>, QueryOptions> OrderBy<T, TProperties>(this IQueryBuilderWithWhere<T, SelectQuery<T>, QueryOptions> queryBuilder, Func<T, TProperties> func, OrderBy orderBy)
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), ErrorMessages.ParameterNotNull);
            }

            return new OrderByQueryBuilder<T>(new DynamicQuery(typeof(T), typeof(TProperties)), orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IAndOr generating a select query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IOrderByQueryBuilder<T, OrderByQuery<T>, QueryOptions> OrderBy<T, TProperties>(this IAndOr<T, SelectQuery<T>, QueryOptions> queryBuilder, Func<T, TProperties> func, OrderBy orderBy)
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), ErrorMessages.ParameterNotNull);
            }
            
            return new OrderByQueryBuilder<T>(new DynamicQuery(typeof(T), typeof(TProperties)), orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilder generating a OrderByQuery query</param>
        /// <param name="func">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IOrderByQueryBuilder<T, TReturn, TQueryOptions> OrderBy<T, TReturn, TQueryOptions, TProperties>(this IOrderByQueryBuilder<T, TReturn, TQueryOptions> queryBuilder, Func<T, TProperties> func, OrderBy orderBy)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), ErrorMessages.ParameterNotNull);
            }

            queryBuilder.AddOrderBy(func, orderBy);

            return queryBuilder;
        }
    }
}