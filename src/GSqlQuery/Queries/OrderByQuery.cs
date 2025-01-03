﻿using GSqlQuery.Cache;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Order by Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class OrderByQuery<T> : Query<T, QueryOptions> where T : class
    {

        /// <summary>
        /// Initializes a new instance of the InsertQuery class.
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="queryOptions">QueryOptions</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal OrderByQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions) :
            base(ref text, table, columns, criteria, queryOptions)
        {
        }
    }
}