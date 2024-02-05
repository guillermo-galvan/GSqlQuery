using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Delete query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class DeleteQuery<T> : Query<T> where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criteria</param>
        /// <param name="formats">Formats</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal DeleteQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats formats) :
            base(ref text, columns, criteria, formats)
        { }
    }
}