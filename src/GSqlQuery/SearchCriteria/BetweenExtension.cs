using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class BetweenExtension
    {
        /// <summary>
        /// Adds the criteria between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Implementation of the IWhere interface</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> Between<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = GSqlQueryExtension.GetAndOr(where,expression);
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            Between<TProperties> between = new Between<TProperties>(columnInfo, where.Formats, initial, final);
            andor.Add(between);
            return andor;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class where TReturn : IQuery<T>
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
            Between<TProperties> between = new Between<TProperties>(columnInfo, andOr.Formats, initial, final, "AND");
            andOr.Add(between);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class where TReturn : IQuery<T>
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
            Between<TProperties> between = new Between<TProperties>(columnInfo, andOr.Formats, initial, final, "OR");
            andOr.Add(between);
            return andOr;
        }
    }
}