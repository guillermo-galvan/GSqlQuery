using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Update query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class UpdateQuery<T> : Query<T, QueryOptions> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the UpdateQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="queryOptions">QueryOptions</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal UpdateQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetail> criteria, QueryOptions queryOptions) :
            base(ref text, columns, criteria, queryOptions)
        { }
    }
}