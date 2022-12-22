using System.Collections.Generic;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria not in(NOT IN)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotIn<T> : In<T>
    {
        protected override string RelationalOperator => "NOT IN";

        protected override string ParameterPrefix => "PI";

        /// <summary>
        /// Initializes a new instance of the NotIn class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="values">Equality value</param>
        public NotIn(TableAttribute table, ColumnAttribute columnAttribute, IEnumerable<T> values) : this(table, columnAttribute, values, null)
        { }

        /// <summary>
        /// Initializes a new instance of the NotIn class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="values">Equality value</param>
        /// <param name="logicalOperator">Logical operator </param>
        /// <exception cref="ArgumentNullException"></exception>
        public NotIn(TableAttribute table, ColumnAttribute columnAttribute, IEnumerable<T> values, string logicalOperator) : base(table, columnAttribute, values, logicalOperator)
        { }
    }
}
