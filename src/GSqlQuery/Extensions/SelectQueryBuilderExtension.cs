﻿using GSqlQuery.Extensions;
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
        public static IQueryBuilderWithWhere<T, CountQuery<T>, IFormats> Count<T>(this IQueryBuilderWithWhere<T, SelectQuery<T>, IFormats> queryBuilder)
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
        public static IQueryBuilder<OrderByQuery<T>, IFormats> OrderBy<T, TProperties>
            (this IQueryBuilderWithWhere<T, SelectQuery<T>, IFormats> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Criteria, options);
            IEnumerable<string> selectMembers = options.MemberInfo.Select(x => x.Name);
            return new OrderByQueryBuilder<T>(selectMembers, orderBy, queryBuilder);
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
        public static IQueryBuilder<OrderByQuery<T>, IFormats> OrderBy<T, TProperties>
            (this IAndOr<T, SelectQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Criteria, options);
            IEnumerable<string> selectMembers = options.MemberInfo.Select(x => x.Name);
            return new OrderByQueryBuilder<T>(selectMembers, orderBy, queryBuilder);
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
        public static IQueryBuilder<OrderByQuery<T>, IFormats> OrderBy<T, TProperties>
            (this IQueryBuilder<OrderByQuery<T>, IFormats> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Criteria, options);

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
        public static IQueryBuilder<OrderByQuery<T>, IFormats> OrderBy<T, TProperties>
            (this IQueryBuilderWithWhere<T, JoinQuery<T>, IFormats> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new JoinOrderByQueryBuilder<T>(options, orderBy, queryBuilder);
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
        public static IQueryBuilder<OrderByQuery<T>, IFormats> OrderBy<T, TProperties>
            (this IAndOr<T, JoinQuery<T>> queryBuilder, Expression<Func<T, TProperties>> expression, OrderBy orderBy)
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
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Criteria, options);
            return new JoinOrderByQueryBuilder<T>(options, orderBy, queryBuilder, queryBuilder.Formats);
        }
    }
}