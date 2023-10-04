using GSqlQuery.Extensions;
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
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            var columnInfo = expression.GetColumnAttribute();
            andor.Add(new NotLike(columnInfo.ClassOptions.Table, columnInfo.MemberInfo, value));
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
            andOr.Validate(expression);
            var columnInfo = expression.GetColumnAttribute();
            andOr.Add(new NotLike(columnInfo.ClassOptions.Table, columnInfo.MemberInfo, value, "AND"));
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
            andOr.Validate(expression);
            var columnInfo = expression.GetColumnAttribute();
            andOr.Add(new NotLike(columnInfo.ClassOptions.Table, columnInfo.MemberInfo, value, "OR"));
            return andOr;
        }
    }
}