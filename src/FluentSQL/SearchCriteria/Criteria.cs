namespace FluentSQL.SearchCriteria
{
    public abstract class Criteria : ISearchCriteria
    {
        /// <summary>
        /// Get Column
        /// </summary>
        public ColumnAttribute Column { get; }

        /// <summary>
        /// Get Table
        /// </summary>
        public TableAttribute Table { get; }

        /// <summary>
        /// Get logical operator
        /// </summary>
        public string? LogicalOperator { get; }

        public Criteria(TableAttribute table, ColumnAttribute columnAttribute, string? logicalOperator)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            Column = columnAttribute ?? throw new ArgumentNullException(nameof(columnAttribute));
            LogicalOperator = logicalOperator;
        }

        public abstract CriteriaDetail GetCriteria(IStatements statements);
    }
}
