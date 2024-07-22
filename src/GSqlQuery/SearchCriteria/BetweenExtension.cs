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
        public static IAndOr<T, TReturn, TQueryOptions> Between<T, TReturn, TQueryOptions,  TProperties>(this IWhere<T, TReturn, TQueryOptions> where, Expression<Func<T, TProperties>> expression, TProperties initial, TProperties final) 
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            IAndOr<T, TReturn, TQueryOptions> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<PropertyOptions> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            Between<TProperties> between = new Between<TProperties>(columnInfo, where.QueryOptions.Formats, initial, final);
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
        public static IAndOr<T, TReturn, TQueryOptions> AndBetween<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> expression, TProperties initial, TProperties final) 
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
            Between<TProperties> between = new Between<TProperties>(columnInfo, andOr.QueryOptions.Formats, initial, final, "AND");
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
        public static IAndOr<T, TReturn, TQueryOptions> OrBetween<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> expression, TProperties initial, TProperties final) 
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
            Between<TProperties> between = new Between<TProperties>(columnInfo, andOr.QueryOptions.Formats, initial, final, "OR");
            andOr.Add(between);
            return andOr;
        }
    }
}