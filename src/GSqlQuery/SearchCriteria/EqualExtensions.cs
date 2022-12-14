using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class EqualExtensions
    {
        /// <summary>
        /// Adds the criteria equal to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> Equal<T, TReturn, TProperties>(this IWhere<T, TReturn> where,
            Expression<Func<T, TProperties>> expression, TProperties value) where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            andor.Add(new Equal<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value));
            return andor;
        }

        /// <summary>
        /// Adds the criteria equal to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Equal<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria equal to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrEqual<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, TProperties value)
            where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Equal<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value, "OR"));
            return andOr;
        }
    }
}
