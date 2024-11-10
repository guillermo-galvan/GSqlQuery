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
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }
            return new CountQueryBuilder<T, TDbConnection>(queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
           (this IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder,
            Expression<Func<T, TProperties>> expression, OrderBy orderBy)
           where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new OrderByQueryBuilder<T, TDbConnection>(options, orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
            (this IAndOr<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new OrderByQueryBuilder<T, TDbConnection>(options, orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T, TProperties, TDbConnection>
            (this IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);

            if (queryBuilder is IOrderByQueryBuilder order)
            {
                order.AddOrderBy(options, orderBy);
            }

            return queryBuilder;
        }

        public static IQueryBuilder<OrderByQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, TProperties, TDbConnection> (this IQueryBuilderWithWhere<Join<T1, T2>, Runner.JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new JoinOrderByQueryBuilder<Join<T1, T2>, TDbConnection>(options, orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, T3, TProperties, TDbConnection>(this IQueryBuilderWithWhere<Join<T1, T2, T3>, Runner.JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2, T3>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>, TDbConnection>(options, orderBy, queryBuilder);
        }

        public static IQueryBuilder<OrderByQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, TProperties, TDbConnection>(this IAndOr<Join<T1, T2>, Runner.JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new JoinOrderByQueryBuilder<Join<T1, T2>, TDbConnection>(options, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }

        public static IQueryBuilder<OrderByQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> OrderBy<T1, T2, T3, TProperties, TDbConnection>(this IAndOr<Join<T1, T2, T3>, Runner.JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder, Expression<Func<Join<T1, T2, T3>, TProperties>> expression, OrderBy orderBy)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>, TDbConnection>(options, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }
    }
}