using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.Runner.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    public static class SelectQueryBuilderExtension
    {
        public static IQueryBuilderWithWhere<T, CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
            Count<T, TDbConnection>(this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            return new CountQueryBuilder<T, TDbConnection>(queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
           (this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
           where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T, TDbConnection>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
            (this IAndOr<T, SelectQuery<T, TDbConnection>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var tmp = queryBuilder.Build();
            return new OrderByQueryBuilder<T, TDbConnection>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder, new ConnectionOptions<TDbConnection>(tmp.Statements, tmp.DatabaseManagement));
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
            (this IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");

            if (queryBuilder is IOrderByQueryBuilder order)
            {
                order.AddOrderBy(options.MemberInfo.Select(x => x.Name), orderBy);
            }
            else if (queryBuilder is IJoinOrderByQueryBuilder join)
            {
                join.AddOrderBy(options, orderBy);
            }

            return queryBuilder;
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
            (this IQueryBuilderWithWhere<T, JoinQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new JoinOrderByQueryBuilder<T, TDbConnection>(options, orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
            (this IAndOr<T, JoinQuery<T, TDbConnection>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var tmp = queryBuilder.Build();
            return new JoinOrderByQueryBuilder<T, TDbConnection>(options, orderBy, queryBuilder, new ConnectionOptions<TDbConnection>(tmp.Statements, tmp.DatabaseManagement));
        }
    }
}