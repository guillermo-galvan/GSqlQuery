using System;

namespace GSqlQuery.MySql
{
    public static class LimitQueryExtension
    {
        public static IQueryBuilder<T, LimitQuery<T>> Limit<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, int start, int? length) where T : class, new()
        {
            if (queryBuilder == null)
            { 
                throw new ArgumentNullException(nameof(queryBuilder));
            }

            return new LimitQueryBuilder<T>(queryBuilder, queryBuilder.Statements, start, length);
        }

        public static IQueryBuilder<T, LimitQuery<T>> Limit<T>(this IAndOr<T, SelectQuery<T>> queryBuilder, int start, int? length) where T : class, new()
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder));
            }

            return new LimitQueryBuilder<T>(queryBuilder, queryBuilder.Build().Statements, start, length);
        }

        public static IQueryBuilder<T, LimitQuery<T>> Limit<T>(this IQueryBuilder<T, OrderByQuery<T>> queryBuilder, int start, int? length) where T : class, new()
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder));
            }
            return new LimitQueryBuilder<T>(queryBuilder, queryBuilder.Statements, start, length);
        }

        public static IQueryBuilder<T, LimitQuery<T, TDbConnection>, TDbConnection> Limit<T, TDbConnection>(
            this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder, int start, int? length) where T : class, new()
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder));
            }

            return new LimitQueryBuilder<T,TDbConnection>(queryBuilder, queryBuilder.ConnectionOptions, start, length);
        }

        public static IQueryBuilder<T, LimitQuery<T, TDbConnection>, TDbConnection> Limit<T, TDbConnection>(
            this IAndOr<T, SelectQuery<T, TDbConnection>> queryBuilder, int start, int? length) where T : class, new()
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder));
            }
            var query = queryBuilder.Build();
            return new LimitQueryBuilder<T, TDbConnection>(queryBuilder, 
                new ConnectionOptions<TDbConnection>(query.Statements, query.DatabaseManagment), start, length);
        }

        public static IQueryBuilder<T, LimitQuery<T, TDbConnection>, TDbConnection> Limit<T, TDbConnection>(
            this IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection> queryBuilder, int start, int? length) where T : class, new()
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder));
            }

            return new LimitQueryBuilder<T, TDbConnection>(queryBuilder, queryBuilder.ConnectionOptions, start, length);
        }
    }
}
