using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class IsNullExtension
    {
        /// <summary>
        /// Adds the criteria is null to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> IsNull<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression)
            where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            andor.Add(new IsNull(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute()));
            return andor;
        }

        /// <summary>
        /// Adds the criteria is null to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndIsNull<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression)
            where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new IsNull(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria is null to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrIsNull<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression)
            where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new IsNull(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), "OR"));
            return andOr;
        }
    }
}
