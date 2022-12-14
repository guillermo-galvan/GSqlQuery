namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(<>)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class NotEqual<T> : Equal<T>, ISearchCriteria
    {
        protected override string ParameterPrefix => "PNE";

        protected override string RelationalOperator => "<>";

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Equality value</param>
        public NotEqual(TableAttribute table, ColumnAttribute columnAttribute, T value) : base(table, columnAttribute, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="table">TableAttribute</param>
        /// <param name="columnAttribute">ColumnAttribute</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public NotEqual(TableAttribute table, ColumnAttribute columnAttribute, T value, string? logicalOperator) : base(table, columnAttribute, value, logicalOperator)
        { }
    }
}
