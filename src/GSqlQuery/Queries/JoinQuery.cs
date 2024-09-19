using GSqlQuery.Cache;
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

        public TableAttribute SecondTable { get; }

        public TableAttribute ThirdTable { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal JoinQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions, TableAttribute secondTable) :
            base(text, table, columns, criteria, queryOptions)
        {
            _options = queryOptions;
            SecondTable = secondTable;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal JoinQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions, TableAttribute secondTable, TableAttribute thirdTable) :
            this(text, table, columns, criteria, queryOptions, secondTable)
        {
            ThirdTable = thirdTable;
        }

        TQueryOptions IQueryOptions<TQueryOptions>.QueryOptions => _options;
    }
}