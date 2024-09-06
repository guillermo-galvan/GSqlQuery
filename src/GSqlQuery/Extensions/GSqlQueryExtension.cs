using System;
using System.Linq.Expressions;

namespace GSqlQuery.Extensions
{
    /// <summary>
    /// GSqlQuery Extension
    /// </summary>
    public static class GSqlQueryExtension
    {
        /// <summary>
        /// Instance of IAndOr
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="func">Expression to evaluate</param>
        /// <returns>Instance of IAndOr</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IAndOr<T, TReturn, TQueryOptions> GetAndOrByFunc<T, TReturn, TQueryOptions, TProperties>(IWhere<T, TReturn, TQueryOptions> where, Func<T, TProperties> func)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            if (where is IAndOr<T, TReturn, TQueryOptions> andor)
            {
                return andor;
            }

            throw new ArgumentNullException(nameof(where));
        }

        public static IAndOr<T, TReturn, TQueryOptions> GetAndOr<T, TReturn, TQueryOptions, TProperties>(IWhere<T, TReturn, TQueryOptions> where)
           where T : class
           where TReturn : IQuery<T, TQueryOptions>
           where TQueryOptions : QueryOptions
        {

            if (where is IAndOr<T, TReturn, TQueryOptions> andor)
            {
                return andor;
            }

            throw new ArgumentNullException(nameof(where));
        }

        /// <summary>
        /// Instance of IAndOr
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        public static void Validate<T, TReturn, TQueryOptions, TProperties>(IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> expression)
            where T : class 
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            if (andOr == null)
            {
                throw new ArgumentNullException(nameof(andOr));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
        }
    }
}