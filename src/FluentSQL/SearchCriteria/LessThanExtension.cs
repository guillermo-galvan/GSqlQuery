using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.SearchCriteria
{
    public static class LessThanExtension
    {
        /// <summary>
        /// Adds the criteria less than to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value </param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> LessThan<T, TProperties>(this IWhere<T> where, Expression<Func<T, TProperties>> expression, TProperties value) where T : class, new()
        {
            IAndOr<T> andor = where.GetAndOr(expression);
            andor.Add(new LessThan<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value));
            return andor;
        }

        /// <summary>
        /// Adds the criteria less than to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> AndLessThan<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression, TProperties value) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new LessThan<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria less than to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> OrLessThan<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression, TProperties value) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new LessThan<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value, "OR"));
            return andOr;
        }
    }
}
