using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Select query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class SelectQuery<T> : Query<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the select query class.
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal SelectQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats formats) :
            base(text, columns, criteria, formats)
        { }
    }
}