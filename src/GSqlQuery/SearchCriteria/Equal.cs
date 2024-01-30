using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(=)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class Equal<T> : Criteria, ISearchCriteria
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
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        public Equal(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T value) : 
            this(classOptionsTupla, formats, value, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public Equal(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T value, string logicalOperator) : 
            base(classOptionsTupla, formats, logicalOperator)
        {
            Value = value;

            string tableName = Table.GetTableName(formats);
            string parameterName = $"@{ParameterPrefix}{Helpers.GetIdParam()}";
            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} {parameterName}" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} {parameterName}";

            _task = Task.FromResult(new CriteriaDetails(criterion, [new ParameterDetail(parameterName, Value, Column.GetPropertyOptions(classOptionsTupla.ClassOptions.PropertyOptions))]));
        }
    }
}