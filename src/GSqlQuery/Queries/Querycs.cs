using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Query<T> : QueryBase, IQuery<T> where T : class, new()
    {
        public IStatements Statements { get; }

        protected ClassOptions GetClassOptions()
        {
            return ClassOptionsFactory.GetClassOptions(typeof(T));
        }

        /// <summary>
        /// Create Query object 
        /// </summary>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>
        /// <param name="text">The Query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, IStatements statements) :
            base(text, columns, criteria)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }
    }
}