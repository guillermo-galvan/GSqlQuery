using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System.Linq.Expressions;

namespace GSqlQuery
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
        public static IAndOr<T, TReturn> In<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
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
        public static IAndOr<T, TReturn> AndIn<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class, new() where TReturn : IQuery
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
        public static IAndOr<T, TReturn> OrIn<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            IEnumerable<TProperties> values) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new In<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), values, "OR"));
            return andOr;
        }
    }
}
