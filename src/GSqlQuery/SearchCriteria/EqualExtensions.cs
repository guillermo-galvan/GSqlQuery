using GSqlQuery.SearchCriteria;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class EqualExtensions
    {
        private static void CreateCriteria<T, TReturn, TQueryOptions, TProperties>(ISearchCriteriaBuilder andOr, IFormats formats, ref Expression<Func<T, TProperties>> func, TProperties value, string logicalOperator)
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

            Equal<T, TProperties> equal = new Equal<T, TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)), formats, value, logicalOperator, ref func);
            andOr.Add(equal);
        }

        /// <summary>
        ///  Adds the criteria equal to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="where">Implementation of the IWhere interface</param>
        /// <param name="func">func to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> Equal<T, TReturn, TQueryOptions, TProperties>(this IWhere<T, TReturn, TQueryOptions> where,
           Expression<Func<T, TProperties>> func, TProperties value)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            CreateCriteria<T, TReturn, TQueryOptions, TProperties>(where, where.QueryOptions.Formats, ref func, value, null);
            return where.AndOr;
        }

        /// <summary>
        /// Adds the criteria equal to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="func">func to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> AndEqual<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> func, TProperties value)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            CreateCriteria<T, TReturn, TQueryOptions, TProperties>(andOr, andOr.QueryOptions.Formats, ref func, value, Constants.AND);
            return andOr;
        }

        /// <summary>
        /// Adds the criteria equal to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TReturn">Query</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="func">func to evaluate</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn, TQueryOptions> OrEqual<T, TReturn, TQueryOptions, TProperties>(this IAndOr<T, TReturn, TQueryOptions> andOr, Expression<Func<T, TProperties>> func, TProperties value)
            where T : class
            where TReturn : IQuery<T, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            CreateCriteria<T, TReturn, TQueryOptions, TProperties>(andOr, andOr.QueryOptions.Formats, ref func, value, Constants.OR);
            return andOr;
        }
    }
}