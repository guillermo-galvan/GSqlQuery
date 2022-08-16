using FluentSQL.Extensions;

namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria is null
    /// </summary>
    public class IsNull : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "IS NULL";

        /// <summary>
        /// Initializes a new instance of the IsNull class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        public IsNull(TableAttribute table, ColumnAttribute columnAttribute) : this(table, columnAttribute, null)
        { }

        /// <summary>
        /// Initializes a new instance of the IsNull class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public IsNull(TableAttribute table, ColumnAttribute columnAttribute, string? logicalOperator) : base(table, columnAttribute, logicalOperator)
        {}

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IStatements statements)
        {
            string tableName = Table.GetTableName(statements);

            string criterion =  string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{tableName}.{Column.GetColumnName(tableName, statements)} {RelationalOperator}" :
                $"{LogicalOperator} {tableName}.{Column.GetColumnName(tableName, statements)} {RelationalOperator}";

            return new CriteriaDetail(this, criterion, Enumerable.Empty<ParameterDetail>());
        }
    }
}
