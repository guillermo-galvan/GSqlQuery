using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria like (LIKE)
    /// </summary>
    public class Like : Criteria
    {
        protected virtual string RelationalOperator => "LIKE";

        protected virtual string ParameterPrefix => "PL";

        /// <summary>
        /// Get Value 
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the Like class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Equality value</param>
        public Like(TableAttribute table, ColumnAttribute columnAttribute, string value) : this(table, columnAttribute, value, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Like class.
        /// </summary>
        /// <param name="table">TableAttribute</param>
        /// <param name="columnAttribute">ColumnAttribute</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public Like(TableAttribute table, ColumnAttribute columnAttribute, string value, string logicalOperator) : base(table, columnAttribute, logicalOperator)
        {
            Value = value;
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IStatements statements, IEnumerable<PropertyOptions> propertyOptions)
        {
            string tableName = Table.GetTableName(statements);
            string parameterName = $"@{ParameterPrefix}{Helpers.GetIdParam()}";
            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, statements, QueryType.Criteria)} {RelationalOperator} CONCAT('%', {parameterName}, '%')" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, statements, QueryType.Criteria)} {RelationalOperator} CONCAT('%', {parameterName}, '%')";

            return new CriteriaDetail(this, criterion, new ParameterDetail[] { new ParameterDetail(parameterName, Value, Column.GetPropertyOptions(propertyOptions)) });
        }
    }
}