using GSqlQuery.Extensions;
using System.Linq.Expressions;
using GSqlQuery.Runner.Default;
using GSqlQuery.Runner.Models;

namespace GSqlQuery.Runner.Extensions
{
    public static class SelectQueryBuilderExtension
    {
        public static IQueryBuilderWithWhere<T, CountQuery<T, TDbConnection>, TDbConnection>
            Count<T, TDbConnection>(this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder) where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            return new CountQueryBuilder<T, TDbConnection>(queryBuilder, queryBuilder.ConnectionOptions);
        }

        public static IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection>
           OrderBy<T, TDbConnection, TProperties>
           (this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder,
           Expression<Func<T, TProperties>> expression, OrderBy orderBy)
           where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T, TDbConnection>(memberInfos.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.ConnectionOptions);
        }

        public static IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection>
            OrderBy<T, TDbConnection, TProperties>
            (this IAndOr<T, SelectQuery<T, TDbConnection>> queryBuilder,
            Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");
            var query = queryBuilder.Build();
            return new OrderByQueryBuilder<T, TDbConnection>(memberInfos.Select(x => x.Name), orderBy, queryBuilder,
                new ConnectionOptions<TDbConnection>(query.Statements, query.DatabaseManagment));
        }

        public static IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection>
            OrderBy<T, TDbConnection, TProperties>
            (this IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection> queryBuilder,
            Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");

            if (queryBuilder is OrderByQueryBuilder<T, TDbConnection> order)
            {
                order.AddOrderBy(memberInfos.Select(x => x.Name), orderBy);
            }

            return queryBuilder;
        }
    }
}
