using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Join Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class JoinQuery<T> : SelectQuery<T> where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal JoinQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, QueryOptions queryOptions) :
            base(text, columns, criteria, queryOptions)
        { }
    }
}