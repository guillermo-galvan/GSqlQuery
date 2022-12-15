namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria Less Than Or Equal(<=)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LessThanOrEqual<T> : Equal<T>
    {
        protected override string ParameterPrefix => "PLTE";

        protected override string RelationalOperator => "<=";

        /// <summary>
        /// Initializes a new instance of the LessThanOrEqual class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Value</param>
        public LessThanOrEqual(TableAttribute table, ColumnAttribute columnAttribute, T value) : base(table, columnAttribute, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the LessThanOrEqual class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public LessThanOrEqual(TableAttribute table, ColumnAttribute columnAttribute, T value, string logicalOperator) : base(table, columnAttribute, value, logicalOperator)
        { }
    }
}
