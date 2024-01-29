using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class NotInExtension
    {
        /// <summary>
        /// Adds the criteria not in to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="values">Values</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> NotIn<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression, IEnumerable<TProperties> values)
            where T : class where TReturn : IQuery<T>
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            var columnInfo = expression.GetColumnAttribute();
            andor.Add(new NotIn<TProperties>(columnInfo, where.Formats, values));
            return andor;
        }

        /// <summary>
        /// Adds the criteria not in to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndNotIn<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class where TReturn : IQuery<T>
        {
            andOr.Validate(expression);
            var columnInfo = expression.GetColumnAttribute();
            andOr.Add(new NotIn<TProperties>(columnInfo, andOr.Formats, values, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria not in to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrNotIn<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class where TReturn : IQuery<T>
        {
            andOr.Validate(expression);
            var columnInfo = expression.GetColumnAttribute();
            andOr.Add(new NotIn<TProperties>(columnInfo, andOr.Formats, values, "OR"));
            return andOr;
        }
    }
}