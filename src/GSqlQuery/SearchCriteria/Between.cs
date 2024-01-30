using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class Between<T> : Criteria, ISearchCriteria
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
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="finalValue">Logical operator</param>
        public Between(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T initialValue, T finalValue) :
            this(classOptionsTupla, formats, initialValue, finalValue, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Between class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="logicalOperator">Logical operator</param>
        public Between(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T initialValue, T finalValue, string logicalOperator) :
            base(classOptionsTupla, formats, logicalOperator)
        {
            Initial = initialValue;
            Final = finalValue;

            string tableName = Table.GetTableName(formats);
            int tiks = Helpers.GetIdParam();
            string parameterName1 = $"@{ParameterPrefix}1{tiks}";
            string parameterName2 = $"@{ParameterPrefix}2{tiks}";

            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} {parameterName1} AND {parameterName2}" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} {parameterName1} AND {parameterName2}";

            var property = Column.GetPropertyOptions(classOptionsTupla.ClassOptions.PropertyOptions);

            _task = Task.FromResult(new CriteriaDetails(criterion, [new ParameterDetail(parameterName1, Initial, property), new ParameterDetail(parameterName2, Final, property)]));
        }
    }
}