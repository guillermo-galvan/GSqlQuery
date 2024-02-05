using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class InExtension
    {
        /// <summary>
        /// Adds the criteria in to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="values">Values</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> In<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = GSqlQueryExtension.GetAndOr(where, expression);
            ClassOptionsTupla<ColumnAttribute> columnInfo = ExpressionExtension.GetColumnAttribute(expression);
            In<TProperties> @in = new In<TProperties>(columnInfo, where.Formats, values);
            andor.Add(@in);
            return andor;
        }

        /// <summary>
        /// Adds the criteria in to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="values">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndIn<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class where TReturn : IQuery<T>
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
            In<TProperties> @in = new In<TProperties>(columnInfo, andOr.Formats, values, "AND");
            andOr.Add(@in);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria in to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="values">Values</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrIn<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class where TReturn : IQuery<T>
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
            In<TProperties> @in = new In<TProperties>(columnInfo, andOr.Formats, values, "OR");
            andOr.Add(@in);
            return andOr;
        }
    }
}