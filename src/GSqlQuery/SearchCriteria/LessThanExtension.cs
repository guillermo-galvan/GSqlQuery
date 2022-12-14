using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System.Linq.Expressions;

namespace GSqlQuery
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
        public static IAndOr<T, TReturn> LessThan<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
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
        public static IAndOr<T, TReturn> AndLessThan<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class, new() where TReturn : IQuery
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
        public static IAndOr<T, TReturn> OrLessThan<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties value) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new LessThan<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value, "OR"));
            return andOr;
        }
    }
}
