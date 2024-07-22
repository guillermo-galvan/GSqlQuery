using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class LessThanOrEqualExtension
    {
        /// <summary>
        /// Adds the criteria less than or equal to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns></returns>
        public static IAndOr<T, TReturn, TQueryOptions> LessThanOrEqual<T, TReturn, TQueryOptions, TProperties>(this IWhere<T, TReturn, TQueryOptions> where, Expression<Func<T, TProperties>> expression,
            TProperties value) where T : class 
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            IAndOr<T, TReturn, TQueryOptions> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<PropertyOptions> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            LessThanOrEqual<TProperties> lessThanOrEqual = new LessThanOrEqual<TProperties>(columnInfo, where.QueryOptions.Formats, value);
            andor.Add(lessThanOrEqual);
            return andor;
        }

        /// <summary>
        /// Adds the criteria less than or equal to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> AndLessThanOrEqual<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> expression,
            TProperties value) 
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
            LessThanOrEqual<TProperties> lessThanOrEqual = new LessThanOrEqual<TProperties>(columnInfo, andOr.QueryOptions.Formats, value, "AND");
            andOr.Add(lessThanOrEqual);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria less than or equal to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> OrLessThanOrEqual<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> expression,
            TProperties value) 
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
            LessThanOrEqual<TProperties> lessThanOrEqual = new LessThanOrEqual<TProperties>(columnInfo, andOr.QueryOptions.Formats, value, "OR");
            andOr.Add(lessThanOrEqual);
            return andOr;
        }
    }
}