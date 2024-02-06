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
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>Instance of IAndOr</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IAndOr<T, TReturn> GetAndOr<T, TReturn, TProperties>(IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression)
           where T : class where TReturn : IQuery<T>
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (where is IAndOr<T, TReturn> andor)
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
        /// <param name="where">Instance of IWhere</param>
        /// <returns>Instance of IAndOr</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IAndOr<T, TReturn> GetAndOr<T, TReturn>(IWhere<T, TReturn> where)
            where T : class where TReturn : IQuery<T>
        {
            if (where is IAndOr<T, TReturn> andor)
            {
                return andor;
            }

            throw new ArgumentNullException(nameof(where), ErrorMessages.ParameterNotNull);
        }

        /// <summary>
        /// Instance of IAndOr
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        public static void Validate<T, TReturn, TProperties>( IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression)
            where T : class where TReturn : IQuery<T>
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