﻿using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class EqualExtensions
    {
        /// <summary>
        ///  Adds the criteria equal to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Implementation of the IWhere interface</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> Equal<T, TReturn, TProperties>(this IWhere<T, TReturn> where,
           Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class
            where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            Equal<TProperties> equal = new Equal<TProperties>(columnInfo, where.Formats, value);
            andor.Add(equal);
            return andor;
        }

        /// <summary>
        /// Adds the criteria equal to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Implementation of the IWhere interface</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class where TReturn : IQuery<T>
        {
            if (andOr == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            if(expression == null) 
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            Equal<TProperties> equal = new Equal<TProperties>(columnInfo, andOr.Formats, value, "AND");
            andOr.Add(equal);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria equal to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Implementation of the IWhere interface</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class where TReturn : IQuery<T>
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
            Equal<TProperties> equal = new Equal<TProperties>(columnInfo, andOr.Formats, value, "OR");
            andOr.Add(equal);
            return andOr;
        }
    }
}