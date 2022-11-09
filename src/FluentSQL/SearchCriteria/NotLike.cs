namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria not like (NOT LIKE)
    /// </summary>
    public class NotLike : Like
    {
        protected override string RelationalOperator => "NOT LIKE";

        protected override string ParameterPrefix => "PNL";

        /// <summary>
        /// Initializes a new instance of the NotLike class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Equality value</param>
        public NotLike(TableAttribute table, ColumnAttribute columnAttribute, string value) : this(table, columnAttribute, value, null)
        { }

        /// <summary>
        /// Initializes a new instance of the NotLike class.
        /// </summary>
        /// <param name="table">TableAttribute</param>
        /// <param name="columnAttribute">ColumnAttribute</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public NotLike(TableAttribute table, ColumnAttribute columnAttribute, string value, string? logicalOperator) : base(table, columnAttribute, value,logicalOperator)
        {}
    }
}
