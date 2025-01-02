using GSqlQuery.Runner.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class JoinOrderByQueryBuilder
    {
        public static IJoinOrderByQueryBuilder<Join<T1, T2>, OrderByQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, TProperties, TDbConnection>(this IQueryBuilderWithWhere<Join<T1, T2>, Runner.JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
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

            return new JoinOrderByQueryBuilder<Join<T1, T2>, TDbConnection>(expression, orderBy, queryBuilder);
        }

        public static IJoinOrderByQueryBuilder<Join<T1, T2, T3>, OrderByQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, T3, TProperties, TDbConnection>(this IQueryBuilderWithWhere<Join<T1, T2, T3>, Runner.JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
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

            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>, TDbConnection>(expression, orderBy, queryBuilder);
        }

        public static IJoinOrderByQueryBuilder<Join<T1, T2>, OrderByQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, TProperties, TDbConnection>(this IAndOr<Join<T1, T2>, Runner.JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
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

            return new JoinOrderByQueryBuilder<Join<T1, T2>, TDbConnection>(expression, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }

        public static IJoinOrderByQueryBuilder<Join<T1, T2, T3>, OrderByQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, T3, TProperties, TDbConnection>(this IAndOr<Join<T1, T2, T3>, Runner.JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2, T3>, TProperties>> expression, OrderBy orderBy)
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

            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>, TDbConnection>(expression, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }
    }
}