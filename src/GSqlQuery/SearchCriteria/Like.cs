using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria like (LIKE)
    /// </summary>
    internal class Like : Criteria
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
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        public Like(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, string value) :
            this(classOptionsTupla, formats, value, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Like class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public Like(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, string value, string logicalOperator) : 
            base(classOptionsTupla, formats, logicalOperator)
        {
            Value = value;

            string tableName = Table.GetTableName(formats);
            string parameterName = $"@{ParameterPrefix}{Helpers.GetIdParam()}";
            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} CONCAT('%', {parameterName}, '%')" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} CONCAT('%', {parameterName}, '%')";

            _task = Task.FromResult(new CriteriaDetails(criterion, [new ParameterDetail(parameterName, Value, Column.GetPropertyOptions(classOptionsTupla.ClassOptions.PropertyOptions))]));
        }
    }
}