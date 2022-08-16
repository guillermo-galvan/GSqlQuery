using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.SearchCriteria
{
    public static class InExtension
    {
        /// <summary>
        /// Adds the criteria in to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="values">Values</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> In<T, TProperties>(this IWhere<T> where, Expression<Func<T, TProperties>> expression, IEnumerable<TProperties> values) where T : class, new()
        {
            IAndOr<T> andor = where.GetAndOr(expression);
            andor.Add(new In<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), values));
            return andor;
        }

        /// <summary>
        /// Adds the criteria in to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="values">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> AndIn<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression, IEnumerable<TProperties> values) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new In<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), values, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria in to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="values">Values</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> OrIn<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression, IEnumerable<TProperties> values) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new In<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), values, "OR"));
            return andOr;
        }
    }
}
