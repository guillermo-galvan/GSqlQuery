using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Query<T> : QueryBase, IQuery<T> where T : class
    {
        /// <summary>
        /// Get Formats
        /// </summary>
        public IFormats Formats { get; }

        /// <summary>
        /// Create Query object 
        /// </summary>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="formats">Formats</param>
        /// <param name="text">The Query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(ref string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats formats) :
            base(ref text, columns, criteria)
        {
            Formats = formats ?? throw new ArgumentNullException(nameof(formats));
        }
    }
}