using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class NotBetweenExtension
    {
        private static void CreateCriteria<T, TReturn, TQueryOptions, TProperties>(ISearchCriteriaBuilder andOr, IFormats formats, ref Expression<Func<T, TProperties>> func, TProperties initial, TProperties final, string logicalOperator)
           where T : class
           where TReturn : IQuery<T, TQueryOptions>
           where TQueryOptions : QueryOptions
        {
            if (andOr == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(andOr), ErrorMessages.ParameterNotNull);
            }

            NotBetween<T, TProperties> equal = new NotBetween<T, TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)), formats, initial, final, logicalOperator, func);
            andOr.Add(equal);
        }

        /// <summary>
        /// Adds the criteria not between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="func">func to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> NotBetween<T, TReturn, TQueryOptions, TProperties>(this IWhere<T, TReturn, TQueryOptions> where, Expression<Func<T, TProperties>> func, TProperties initial, TProperties final)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            CreateCriteria<T, TReturn, TQueryOptions, TProperties>(where, where.QueryOptions.Formats, ref func, initial, final, null);
            return where.AndOr;
        }

        /// <summary>
        /// Adds the criteria not between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="func">func to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> AndNotBetween<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> func, TProperties initial, TProperties final)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            CreateCriteria<T, TReturn, TQueryOptions, TProperties>(andOr, andOr.QueryOptions.Formats, ref func, initial, final, Constants.AND);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria not between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="func">func to evaluate</param>
        /// <param name="initial">Initial value </param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> OrNotBetween<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> func, TProperties initial, TProperties final)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            CreateCriteria<T, TReturn, TQueryOptions, TProperties>(andOr, andOr.QueryOptions.Formats, ref func, initial, final, Constants.OR);
            return andOr;
        }
    }
}