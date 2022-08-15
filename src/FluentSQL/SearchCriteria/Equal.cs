using FluentSQL.Extensions;

namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(=)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class Equal<T> : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "=";

        protected virtual string ParameterPrefix => "PE";

        /// <summary>
        /// Value for equality
        /// </summary>
        public T Value { get; }

        public Equal(TableAttribute table, ColumnAttribute columnAttribute, T value) : this(table,columnAttribute,value,null)
        {}

        public Equal(TableAttribute table, ColumnAttribute columnAttribute,T value,string? logicalOperator) : base(table,columnAttribute, logicalOperator)
        {
            Value = value;
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements to use in the query</param>
        /// <returns></returns>
        public override CriteriaDetail GetCriteria(IStatements statements)
        {
            string tableName = Table.GetTableName(statements);

            string parameterName = $"@{ParameterPrefix}{DateTime.Now.Ticks}";
            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ? 
                $"{tableName}.{Column.GetColumnName(tableName, statements)} {RelationalOperator} {parameterName}" :
                $"{LogicalOperator} {tableName}.{Column.GetColumnName(tableName, statements)} {RelationalOperator} {parameterName}";

            return new CriteriaDetail(this, criterion, parameterName, Value);
        }
    }
}
