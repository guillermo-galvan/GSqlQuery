using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class JoinOrderByQueryBuilder
    {
        public static IJoinOrderByQueryBuilder<T, TReturn, TQueryOptions> OrderBy<T, TReturn, TQueryOptions, TProperties>(this IJoinOrderByQueryBuilder<T, TReturn, TQueryOptions> joinOrderBy, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {

            if (joinOrderBy == null)
            {
                throw new ArgumentNullException(nameof(joinOrderBy), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            joinOrderBy.AddOrderBy(expression, orderBy);

            return joinOrderBy;
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IJoinOrderByQueryBuilder<Join<T1, T2>, OrderByQuery<Join<T1, T2>>, QueryOptions> OrderBy<T1, T2, TProperties>(this IQueryBuilderWithWhere<Join<T1, T2>, JoinQuery<Join<T1, T2>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            return new JoinOrderByQueryBuilder<Join<T1, T2>>(expression, orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>5
        /// <exception cref="ArgumentNullException"></exception>
        public static IJoinOrderByQueryBuilder<Join<T1, T2, T3>, OrderByQuery<Join<T1, T2, T3>>, QueryOptions> OrderBy<T1, T2, T3, TProperties>(this IQueryBuilderWithWhere<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2, T3>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>>(expression, orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IAndOr generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IJoinOrderByQueryBuilder<Join<T1, T2>, OrderByQuery<Join<T1, T2>>, QueryOptions> OrderBy<T1, T2, TProperties>(this IAndOr<Join<T1, T2>, JoinQuery<Join<T1, T2>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            return new JoinOrderByQueryBuilder<Join<T1, T2>>(expression, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IAndOr generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IJoinOrderByQueryBuilder<Join<T1, T2, T3>, OrderByQuery<Join<T1, T2, T3>>, QueryOptions> OrderBy<T1, T2, T3, TProperties>(this IAndOr<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2, T3>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>>(expression, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }
    }
}
