using FluentSQL.Default;

namespace FluentSQL.Extensions
{
    public static class SelectQueryBuilderExtension
    {
        public static IQueryBuilderWithWhere<T, CountQuery<T>> Count<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder) where T : class, new()
        {
            queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
            return new CountQueryBuilder<T>(queryBuilder, queryBuilder.Statements);
        }
    }
}
