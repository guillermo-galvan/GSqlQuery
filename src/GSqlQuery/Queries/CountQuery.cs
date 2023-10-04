using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Count query
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    public class CountQuery<T> : Query<T> where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal CountQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats formats) :
            base(text, columns, criteria, formats)
        {
        }
    }
}