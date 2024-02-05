using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class NotBetweenExtension
    {
        /// <summary>
        /// Adds the criteria not between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> NotBetween<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            NotBetween<TProperties> notBetween = new NotBetween<TProperties>(columnInfo, where.Formats, initial, final);
            andor.Add(notBetween);
            return andor;
        }

        /// <summary>
        /// Adds the criteria not between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndNotBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
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
            NotBetween<TProperties> notBetween = new NotBetween<TProperties>(columnInfo, andOr.Formats, initial, final, "AND");
            andOr.Add(notBetween);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria not between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value </param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrNotBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
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
            NotBetween<TProperties> notBetween = new NotBetween<TProperties>(columnInfo, andOr.Formats, initial, final, "OR");
            andOr.Add(notBetween);
            return andOr;
        }
    }
}