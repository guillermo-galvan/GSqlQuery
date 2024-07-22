﻿using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class GreaterThanExtension
    {
        /// <summary>
        /// Adds the criteria greater than to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> GreaterThan<T, TReturn, TQueryOptions, TProperties>(this IWhere<T, TReturn, TQueryOptions> where,
            Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class 
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            IAndOr<T, TReturn, TQueryOptions> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<PropertyOptions> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            GreaterThan<TProperties> greaterThan = new GreaterThan<TProperties>(columnInfo, where.QueryOptions.Formats, value);
            andor.Add(greaterThan);
            return andor;
        }

        /// <summary>
        /// Adds the criteria greater than to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> AndGreaterThan<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> expression, TProperties value) 
            where T : class 
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            if (andOr == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<PropertyOptions> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            GreaterThan<TProperties> greaterThan = new GreaterThan<TProperties>(columnInfo, andOr.QueryOptions.Formats, value, "AND");
            andOr.Add(greaterThan);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria greater than to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value/param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> OrGreaterThan<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> expression, TProperties value) 
            where T : class 
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            if (andOr == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<PropertyOptions> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            GreaterThan<TProperties> greaterThan = new GreaterThan<TProperties>(columnInfo, andOr.QueryOptions.Formats, value, "OR");
            andOr.Add(greaterThan);
            return andOr;
        }
    }
}