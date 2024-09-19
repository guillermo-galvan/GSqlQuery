using GSqlQuery.Cache;
using System.Collections.Generic;
using System.Linq;

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
        internal UpdateQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions) :
            base(ref text, table, columns, criteria, queryOptions)
        { }
    }
}