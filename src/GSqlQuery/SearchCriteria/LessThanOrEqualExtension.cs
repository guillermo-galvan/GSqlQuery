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
        public static IAndOr<T, TReturn> LessThanOrEqual<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression,
            TProperties value) where T : class where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            LessThanOrEqual<TProperties> lessThanOrEqual = new LessThanOrEqual<TProperties>(columnInfo, where.Formats, value);
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
        public static IAndOr<T, TReturn> AndLessThanOrEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties value) where T : class where TReturn : IQuery<T>
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
            LessThanOrEqual<TProperties> lessThanOrEqual = new LessThanOrEqual<TProperties>(columnInfo, andOr.Formats, value, "AND");
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
        public static IAndOr<T, TReturn> OrLessThanOrEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties value) where T : class where TReturn : IQuery<T>
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
            LessThanOrEqual<TProperties> lessThanOrEqual = new LessThanOrEqual<TProperties>(columnInfo, andOr.Formats, value, "OR");
            andOr.Add(lessThanOrEqual);
            return andOr;
        }
    }
}