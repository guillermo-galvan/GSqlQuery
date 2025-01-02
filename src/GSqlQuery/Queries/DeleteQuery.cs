using GSqlQuery.Cache;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery
{
    /// <summary>
    /// Delete query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class DeleteQuery<T> : Query<T, QueryOptions> where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="queryOptions">QueryOptions</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal DeleteQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions) :
            base(ref text, table, columns, criteria, queryOptions)
        { }
    }
}