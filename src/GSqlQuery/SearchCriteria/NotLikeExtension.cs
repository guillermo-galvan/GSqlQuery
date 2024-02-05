﻿using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class NotLikeExtension
    {
        /// <summary>
        /// Adds the criteria not like to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> NotLike<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression, string value)
            where T : class where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            NotLike notLike = new NotLike(columnInfo, where.Formats, value);
            andor.Add(notLike);
            return andor;
        }

        /// <summary>
        /// Adds the criteria not like to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndNotLike<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            string value) where T : class where TReturn : IQuery<T>
        {
            if (andOr == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            NotLike notLike = new NotLike(columnInfo, andOr.Formats, value, "AND");
            andOr.Add(notLike);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria not like to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrNotLike<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            string value) where T : class where TReturn : IQuery<T>
        {
            if (andOr == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            NotLike notLike = new NotLike(columnInfo, andOr.Formats, value, "OR");
            andOr.Add(notLike);
            return andOr;
        }
    }
}