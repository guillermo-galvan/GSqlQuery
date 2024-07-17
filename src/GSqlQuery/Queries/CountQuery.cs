using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Count query
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    public class CountQuery<T> : Query<T, QueryOptions> where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="queryOptions">QueryOptions</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal CountQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetail> criteria, QueryOptions queryOptions) :
            base(ref text, columns, criteria, queryOptions)
        {
        }
    }
}