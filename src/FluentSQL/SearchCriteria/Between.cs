using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class Between<T> : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "BETWEEN";

        protected virtual string ParameterPrefix => "PB";

        /// <summary>
        /// Get the initial value
        /// </summary>
        public T Initial { get; }

        /// <summary>
        /// Get the final value
        /// </summary>
        public T Final { get; }

        /// <summary>
        /// Initializes a new instance of the Between class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="initialValue">Initial value</param>
        public Between(TableAttribute table, ColumnAttribute columnAttribute, T initialValue, T finalValue) : this(table, columnAttribute, initialValue, finalValue, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Between class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="logicalOperator">Logical operator</param>
        public Between(TableAttribute table, ColumnAttribute columnAttribute, T initialValue, T finalValue, string? logicalOperator) : 
            base(table, columnAttribute, logicalOperator)
        {
            Initial = initialValue;
            Final = finalValue;
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IStatements statements, IEnumerable<PropertyOptions> propertyOptions)
        {
            string tableName = Table.GetTableName(statements);
            long tiks = DateTime.Now.Ticks;
            string parameterName1 = $"@{ParameterPrefix}1{tiks}";
            string parameterName2 = $"@{ParameterPrefix}2{tiks}";

            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, statements)} {RelationalOperator} {parameterName1} AND {parameterName2}" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, statements)} {RelationalOperator} {parameterName1} AND {parameterName2}";

            var property = Column.GetPropertyOptions(propertyOptions);

            return new CriteriaDetail(this, criterion, new ParameterDetail[]
            {
                new ParameterDetail(parameterName1, Initial,property),
                new ParameterDetail(parameterName2, Final,property)
            });
        }
    }
}
