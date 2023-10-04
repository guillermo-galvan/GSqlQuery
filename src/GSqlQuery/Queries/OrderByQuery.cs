using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Order by Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class OrderByQuery<T> : Query<T> where T : class
    {

        /// <summary>
        /// Initializes a new instance of the InsertQuery class.
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal OrderByQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats formats) :
            base(text, columns, criteria, formats)
        {
        }
    }
}