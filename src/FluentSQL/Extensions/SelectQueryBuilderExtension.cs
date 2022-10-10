using FluentSQL.Default;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

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

        public static IQueryBuilder<T, OrderByQuery<T>> OrderBy<T, TProperties>
            (this IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy) 
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T>(memberInfos.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.Statements );
        }

        public static IQueryBuilder<T, OrderByQuery<T>> OrderBy<T, TProperties>
            (this IAndOr<T, SelectQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T>(memberInfos.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.Build().Statements);
        }

        public static IQueryBuilder<T, OrderByQuery<T>> OrderBy<T, TProperties>
            (this IQueryBuilder<T, OrderByQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");

            if (queryBuilder is OrderByQueryBuilder<T> order)
            {
                order.AddOrderBy(memberInfos.Select(x => x.Name), orderBy);
            }

            return queryBuilder;
        }

        public static IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> 
            OrderBy<T, TDbConnection,TProperties>
            (this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder, 
            Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T, TDbConnection>(memberInfos.Select(x => x.Name),orderBy,queryBuilder,  queryBuilder.ConnectionOptions);
        }

        public static IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>>
            OrderBy<T, TDbConnection, TProperties>
            (this IAndOr<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder,
            Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T, TDbConnection>(memberInfos.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.Build().ConnectionOptions);
        }

        public static IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>>
            OrderBy<T, TDbConnection, TProperties>
            (this IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder,
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
