namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Less Than (<)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class LessThan<T> : Equal<T>
    {
        protected override string ParameterPrefix => "PLT";

        protected override string RelationalOperator => "<";

        /// <summary>
        /// Initializes a new instance of the LessThan class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Value</param>
        public LessThan(TableAttribute table, ColumnAttribute columnAttribute, T value) : base(table, columnAttribute, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the LessThan class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Value</param>
        /// <param name="logicalOperator">Logical operator</param>
        public LessThan(TableAttribute table, ColumnAttribute columnAttribute, T value, string? logicalOperator) : base(table, columnAttribute, value, logicalOperator)
        { }
    }
}
