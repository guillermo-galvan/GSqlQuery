using GSqlQuery.Extensions;
using System.Linq.Expressions;
using GSqlQuery.Runner.Queries;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using GSqlQuery.Runner;

namespace GSqlQuery
{
    public static class SelectQueryBuilderExtension
    {
        public static IQueryBuilderWithWhereRunner<T, CountQuery<T, TDbConnection>, TDbConnection>
            Count<T, TDbConnection>(this IQueryBuilderWithWhereRunner<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder) where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            return new CountQueryBuilder<T, TDbConnection>(queryBuilder, queryBuilder.ConnectionOptions);
        }

        public static IQueryBuilderRunner<T, OrderByQuery<T, TDbConnection>, TDbConnection>
           OrderBy<T, TDbConnection, TProperties>
           (this IQueryBuilderWithWhereRunner<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder,
           Expression<Func<T, TProperties>> expression, OrderBy orderBy)
           where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T, TDbConnection>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.ConnectionOptions);
        }

        public static IQueryBuilderRunner<T, OrderByQuery<T, TDbConnection>, TDbConnection>
            OrderBy<T, TDbConnection, TProperties>
            (this IAndOr<T, SelectQuery<T, TDbConnection>> queryBuilder,
            Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var query = queryBuilder.Build();
            return new OrderByQueryBuilder<T, TDbConnection>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder,
                new ConnectionOptions<TDbConnection>(query.Statements, query.DatabaseManagement));
        }

        public static IQueryBuilderRunner<T, OrderByQuery<T, TDbConnection>, TDbConnection>
            OrderBy<T, TDbConnection, TProperties>
            (this IQueryBuilderRunner<T, OrderByQuery<T, TDbConnection>, TDbConnection> queryBuilder,
            Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");

            if (queryBuilder is OrderByQueryBuilder<T, TDbConnection> order)
            {
                order.AddOrderBy(options.MemberInfo.Select(x => x.Name), orderBy);
            }

            return queryBuilder;
        }
    }
}
