using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery
{
    /// <summary>
    /// Select Query Builder Extension
    /// </summary>
    public static class SelectQueryBuilderExtension
    {
        /// <summary>
        /// Generate the count query.
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a select query</param>
        /// <returns>IQueryBuilderWithWhere&lt;<typeparamref name="T"/>,CountQuery&lt;<typeparamref name="T"/>&gt;&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilderWithWhere<T, CountQuery<T>, QueryOptions> Count<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>, QueryOptions> queryBuilder)
            where T : class
        {
            if (queryBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryBuilder), ErrorMessages.ParameterNotNull);
            }
            return new CountQueryBuilder<T>(queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a select query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilder<OrderByQuery<T>, QueryOptions> OrderBy<T, TProperties>
            (this IQueryBuilderWithWhere<T, SelectQuery<T>, QueryOptions> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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
            return new OrderByQueryBuilder<T>(options, orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IAndOr generating a select query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilder<OrderByQuery<T>, QueryOptions> OrderBy<T, TProperties>
            (this IAndOr<T, SelectQuery<T>, QueryOptions> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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
            return new OrderByQueryBuilder<T>(options, orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilder generating a OrderByQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilder<OrderByQuery<T>, QueryOptions> OrderBy<T, TProperties>
            (this IQueryBuilder<OrderByQuery<T>, QueryOptions> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilder<OrderByQuery<Join<T1, T2>>, QueryOptions> OrderBy<T1,T2, TProperties>
            (this IQueryBuilderWithWhere<Join<T1, T2>, JoinQuery<Join<T1, T2>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
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
            return new JoinOrderByQueryBuilder<Join<T1, T2>>(options, orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IQueryBuilderWithWhere generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilder<OrderByQuery<Join<T1, T2, T3>>, QueryOptions> OrderBy<T1, T2, T3, TProperties>
            (this IQueryBuilderWithWhere<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
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
            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>>(options, orderBy, queryBuilder);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IAndOr generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilder<OrderByQuery<Join<T1, T2>>, QueryOptions> OrderBy<T1, T2, TProperties>
            (this IAndOr<Join<T1, T2>, JoinQuery<Join<T1, T2>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2>, TProperties>> expression, OrderBy orderBy)
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
            return new JoinOrderByQueryBuilder<Join<T1, T2>>(options, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }

        /// <summary>
        /// Add the 'order by' to the query
        /// </summary>
        /// <typeparam name="T">Type to create the query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="queryBuilder">IAndOr generating a JoinQuery query</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="orderBy">Order By type</param>
        /// <returns>IQueryBuilder&lt;OrderByQuery&lt;<typeparamref name="T"/>&gt;, IFormats&gt;</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryBuilder<OrderByQuery<Join<T1, T2, T3>>, QueryOptions> OrderBy<T1, T2, T3, TProperties>
            (this IAndOr<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, QueryOptions>, QueryOptions> queryBuilder, Expression<Func<Join<T1, T2, T3>, TProperties>> expression, OrderBy orderBy)
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
            return new JoinOrderByQueryBuilder<Join<T1, T2, T3>>(options, orderBy, queryBuilder, queryBuilder.QueryOptions);
        }
    }
}