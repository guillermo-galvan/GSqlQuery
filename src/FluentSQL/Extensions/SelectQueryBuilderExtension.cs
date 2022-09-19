using FluentSQL.Default;

namespace FluentSQL.Extensions
{
    public static class SelectQueryBuilderExtension
    {
        public static IQueryBuilderWithWhere<T, CountQuery<T>> Count<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder) where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            return new CountQueryBuilder<T>(queryBuilder, queryBuilder.Statements);
        }

        public static IQueryBuilderWithWhere<T, CountQuery<T, TDbConnection>, TDbConnection, int> 
            Count<T, TDbConnection>(this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder) where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            return new CountQueryBuilder<T, TDbConnection>(queryBuilder, queryBuilder.ConnectionOptions);
        }
    }
}
