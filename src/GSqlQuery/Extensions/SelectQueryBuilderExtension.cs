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
        public static IQueryBuilderWithWhere<T, CountQuery<T>> Count<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder) where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            return new CountQueryBuilder<T>(queryBuilder, queryBuilder.Options);
        }

        public static IQueryBuilder<T, OrderByQuery<T>> OrderBy<T, TProperties>
            (this IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.Options);
        }

        public static IQueryBuilder<T, OrderByQuery<T>> OrderBy<T, TProperties>
            (this IAndOr<T, SelectQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            return new OrderByQueryBuilder<T>(options.MemberInfo.Select(x => x.Name), orderBy, queryBuilder, queryBuilder.Build().Statements);
        }

        public static IQueryBuilder<T, OrderByQuery<T>> OrderBy<T, TProperties>
            (this IQueryBuilder<T, OrderByQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class, new()
        {
            queryBuilder.NullValidate(ErrorMessages.ParameterNotNull, nameof(queryBuilder));
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");

            if (queryBuilder is OrderByQueryBuilder<T> order)
            {
                order.AddOrderBy(options.MemberInfo.Select(x => x.Name), orderBy);
            }

            return queryBuilder;
        }
    }
}
