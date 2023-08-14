using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    public static class SelectQueryBuilderExtension
    {
        public static IQueryBuilderWithWhere<T, CountQuery<T>, IStatements> Count<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>, IStatements> queryBuilder)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            return new CountQueryBuilder<T>(queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T>, IStatements> OrderBy<T, TProperties>
            (this IQueryBuilderWithWhere<T, SelectQuery<T>, IStatements> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T>, IStatements> OrderBy<T, TProperties>
            (this IAndOr<T, SelectQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.Build().Statements);
        }

        public static IQueryBuilder<OrderByQuery<T>, IStatements> OrderBy<T, TProperties>
            (this IQueryBuilder<OrderByQuery<T>, IStatements> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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

        public static IQueryBuilder<OrderByQuery<T>, IStatements> OrderBy<T, TProperties>
            (this IQueryBuilderWithWhere<T, JoinQuery<T>, IStatements> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new JoinOrderByQueryBuilder<T>(options, orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T>, IStatements> OrderBy<T, TProperties>
            (this IAndOr<T, JoinQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new JoinOrderByQueryBuilder<T>(options, orderBy, queryBuilder, queryBuilder.Build().Statements);
        }
    }
}