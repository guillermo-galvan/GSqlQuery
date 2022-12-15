namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Greater Than Or Equal(>=)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GreaterThanOrEqual<T> : Equal<T>
    {
        protected override string ParameterPrefix => "PGTE";

        protected override string RelationalOperator => ">=";

        /// <summary>
        /// Initializes a new instance of the GreaterThanOrEqual class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Value</param>
        public GreaterThanOrEqual(TableAttribute table, ColumnAttribute columnAttribute, T value) : base(table, columnAttribute, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GreaterThanOrEqual class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public GreaterThanOrEqual(TableAttribute table, ColumnAttribute columnAttribute, T value, string logicalOperator) : base(table, columnAttribute, value, logicalOperator)
        { }
    }
}
