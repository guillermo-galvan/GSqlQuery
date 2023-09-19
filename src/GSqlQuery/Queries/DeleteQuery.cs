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
        /// Initializes a new instance of the DeleteQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        internal DeleteQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        { }
    }


}