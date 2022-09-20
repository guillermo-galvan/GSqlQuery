using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.SearchCriteria
{
    public static class BetweenExtension
    {
        /// <summary>
        /// Adds the criteria between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> Between<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression, 
            TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            andor.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial,final));
            return andor;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression, 
            TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "OR"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn> Between<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression,
            TProperties initial) where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            andor.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial));
            return andor;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn> AndBetween<T, TReturn,TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn> OrBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, "OR"));
            return andOr;
        }

        public static IAndOr<T, TReturn, TDbConnection, TResult> Between<T, TReturn, TDbConnection, TResult, TProperties>(this IWhere<T, TReturn, TDbConnection, TResult> where, 
            Expression<Func<T, TProperties>> expression, TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn, TDbConnection, TResult> andor = where.GetAndOr(expression);
            andor.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final));
            return andor;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TDbConnection, TResult> AndBetween<T, TReturn, TDbConnection, TResult, TProperties>(this IAndOr<T, TReturn, TDbConnection, TResult> andOr,
            Expression<Func<T, TProperties>> expression, TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TDbConnection, TResult> OrBetween<T, TReturn, TDbConnection, TResult, TProperties>(this IAndOr<T, TReturn, TDbConnection, TResult> andOr,
            Expression<Func<T, TProperties>> expression, TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "OR"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn, TDbConnection, TResult> Between<T, TReturn, TDbConnection, TResult, TProperties>(this IWhere<T, TReturn, TDbConnection, TResult> where, 
            Expression<Func<T, TProperties>> expression, TProperties initial) where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn, TDbConnection, TResult> andor = where.GetAndOr(expression);
            andor.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial));
            return andor;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn, TDbConnection, TResult> AndBetween<T, TReturn, TDbConnection, TResult, TProperties>(this IAndOr<T, TReturn, TDbConnection, TResult> andOr,
            Expression<Func<T, TProperties>> expression, TProperties initial) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T, TReturn, TDbConnection, TResult> OrBetween<T, TReturn, TDbConnection, TResult, TProperties>(this IAndOr<T, TReturn, TDbConnection, TResult> andOr,
            Expression<Func<T, TProperties>> expression, TProperties initial) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, "OR"));
            return andOr;
        }
    }
}
