using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Select query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class SelectQuery<T> : Query<T> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the SelectQuery class.
        /// </summary>
        /// <param name="text">The Query</param>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>        
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        { }
    }
}
