using GSqlQuery.Extensions;
using System;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Initializes a new instance of the Criteria class.
    /// </summary>
    /// <param name="classOptionsTupla">ClassOptionsTupla<ColumnAttribute></param>
    /// <param name="formats">Column Attribute</param>
    /// <param name="logicalOperator">Logical Operator </param>
    /// <exception cref="ArgumentNullException"></exception>
    internal abstract class Criteria(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, string logicalOperator) : ISearchCriteria
    {
        protected readonly ClassOptionsTupla<ColumnAttribute> _classOptionsTupla = classOptionsTupla;

        protected Task<CriteriaDetails> _task;

        /// <summary>
        /// Get Column
        /// </summary>
        public ColumnAttribute Column { get; } = classOptionsTupla.Columns;

        /// <summary>
        /// Get Table
        /// </summary>
        public TableAttribute Table { get; } = classOptionsTupla.ClassOptions.Table;

        /// <summary>
        /// Get logical operator
        /// </summary>
        public string LogicalOperator { get; } = logicalOperator;

        /// <summary>
        /// Get Formats
        /// </summary>
        public IFormats Formats { get; } = formats;

        /// <summary>
        /// Get ClassOptions
        /// </summary>
        public ClassOptions ClassOptions => _classOptionsTupla.ClassOptions;

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Details of the criteria</returns>
        public virtual CriteriaDetail GetCriteria()
        {
            _task.Wait();
            return new CriteriaDetail(this, _task.Result.Criterion, _task.Result.Parameters);
        }

        /// <summary>
        /// Find the properties that receive in the column parameter.
        /// </summary>
        /// <param name="column">Contains the property information</param>
        /// <param name="propertyOptions">List of property options</param>
        /// <returns>Property options</returns>
        protected PropertyOptions GetPropertyOptions(ColumnAttribute column, PropertyOptionsCollection propertyOptions)
        {
            return propertyOptions.GetValue(column);
        }
    }
}