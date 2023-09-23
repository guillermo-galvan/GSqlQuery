using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Insert Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class InsertQuery<T> : Query<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the InsertQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal InsertQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats statements, object entity)
            : base(text, columns, criteria, statements)
        { }
    }
}