using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class IsNullExtension
    {
        /// <summary>
        /// Adds the criteria is null to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> IsNull<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression)
            where T : class where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            IsNull isNull = new IsNull(columnInfo, where.Formats);
            andor.Add(isNull);
            return andor;
        }

        /// <summary>
        /// Adds the criteria is null to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndIsNull<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression)
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
            IsNull isNull = new IsNull(columnInfo, andOr.Formats, "AND");
            andOr.Add(isNull);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria is null to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrIsNull<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression)
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
            IsNull isNull = new IsNull(columnInfo, andOr.Formats, "OR");
            andOr.Add(isNull);
            return andOr;
        }
    }
}