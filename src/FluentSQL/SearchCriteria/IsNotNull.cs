namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria is not null
    /// </summary>
    public class IsNotNull : IsNull
    {
        protected override string RelationalOperator => "IS NOT NULL";

        /// <summary>
        /// Initializes a new instance of the IsNotNull class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        public IsNotNull(TableAttribute table, ColumnAttribute columnAttribute) : this(table, columnAttribute, null)
        { }

        /// <summary>
        /// Initializes a new instance of the IsNotNull class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public IsNotNull(TableAttribute table, ColumnAttribute columnAttribute, string? logicalOperator) : base(table, columnAttribute, logicalOperator)
        { }
    }
}
