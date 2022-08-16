namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria NOT BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class NotBetween<T> : Between<T>
    {
        protected override string RelationalOperator => "NOT BETWEEN";

        protected override string ParameterPrefix => "PNB";

        /// <summary>
        /// Initializes a new instance of the NotBetween class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="initialValue">Initial value</param>
        public NotBetween(TableAttribute table, ColumnAttribute columnAttribute, T initialValue) : this(table, columnAttribute, initialValue, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the NotBetween class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="logicalOperator">Logical operator</param>
        public NotBetween(TableAttribute table, ColumnAttribute columnAttribute, T initialValue, string? logicalOperator) : base(table, columnAttribute, initialValue, logicalOperator)
        {
        }
    }

    /// <summary>
    ///  Represents the search criteria NOT BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class NotBetween2<T> : Between2<T>
    {
        protected override string RelationalOperator => "NOT BETWEEN";

        protected override string ParameterPrefix => "PNB";

        /// <summary>
        /// Initializes a new instance of the NotBetween2 class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="finalValue">Final value</param>
        public NotBetween2(TableAttribute table, ColumnAttribute columnAttribute, T initialValue, T finalValue) : this(table, columnAttribute, initialValue, finalValue,null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the NotBetween2 class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="finalValue">Final value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public NotBetween2(TableAttribute table, ColumnAttribute columnAttribute, T initialValue, T finalValue, string? logicalOperator) 
            : base(table, columnAttribute, initialValue, finalValue,logicalOperator)
        {
        }
    }
}
