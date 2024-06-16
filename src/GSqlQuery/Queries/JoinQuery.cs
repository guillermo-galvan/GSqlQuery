using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Join Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class JoinQuery<T, TQueryOptions> : SelectQuery<T>, IQuery<T, TQueryOptions>
        where T : class
        where TQueryOptions : QueryOptions
    {
        private readonly TQueryOptions _options;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal JoinQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, TQueryOptions queryOptions) :
            base(text, columns, criteria, queryOptions)
        {
            _options = queryOptions;
        }

        TQueryOptions IQueryOptions<TQueryOptions>.QueryOptions => _options;
    }
}