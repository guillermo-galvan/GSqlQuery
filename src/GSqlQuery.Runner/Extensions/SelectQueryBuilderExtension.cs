using GSqlQuery.Queries;
using GSqlQuery.Runner.Queries;
using System;

namespace GSqlQuery
{
    public static class SelectQueryBuilderExtension
    {
        public static IQueryBuilderWithWhere<T, CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> Count<T, TDbConnection>(this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder)
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }
            return new CountQueryBuilder<T, TDbConnection>(queryBuilder);
        }

        public static IOrderByQueryBuilder<T, OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>(this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Func<T, TProperties> func, OrderBy orderBy)
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

            return new OrderByQueryBuilder<T, TDbConnection>(new DynamicQuery(typeof(T), typeof(TProperties)), orderBy, queryBuilder);
        }

        public static IOrderByQueryBuilder<T, OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
            (this IAndOr<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Func<T, TProperties> func, OrderBy orderBy)
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

            return new OrderByQueryBuilder<T, TDbConnection>(new DynamicQuery(typeof(T), typeof(TProperties)), orderBy, queryBuilder);
        }
    }
}