﻿using GSqlQuery.Extensions;
using System.Collections.Generic;

namespace GSqlQuery.SearchCriteria
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
        /// Get equality value
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Equality value</param>
        public Equal(TableAttribute table, ColumnAttribute columnAttribute, T value) : this(table, columnAttribute, value, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="table">TableAttribute</param>
        /// <param name="columnAttribute">ColumnAttribute</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public Equal(TableAttribute table, ColumnAttribute columnAttribute, T value, string logicalOperator) : base(table, columnAttribute, logicalOperator)
        {
            Value = value;
        }

        /// <summary>
        /// Get Criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IFormats formats, IEnumerable<PropertyOptions> propertyOptions)
        {
            string tableName = Table.GetTableName(formats);

            string parameterName = $"@{ParameterPrefix}{Helpers.GetIdParam()}";
            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} {parameterName}" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} {parameterName}";

            return new CriteriaDetail(this, criterion, new ParameterDetail[] { new ParameterDetail(parameterName, Value, Column.GetPropertyOptions(propertyOptions)) });
        }
    }
}