using GSqlQuery.Extensions;
using System;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Initializes a new instance of the Criteria class.
    /// </summary>
    /// <param name="classOptionsTupla">ClassOptionsTupla<ColumnAttribute></param>
    /// <param name="formats">Column Attribute</param>
    /// <param name="logicalOperator">Logical Operator </param>
    /// <exception cref="ArgumentNullException"></exception>
    internal abstract class Criteria(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, string logicalOperator) : ISearchCriteria
    {
        protected readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla = classOptionsTupla;

        /// <summary>
        /// Get Column
        /// </summary>
        public ColumnAttribute Column { get; } = classOptionsTupla.Columns.ColumnAttribute;

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
        public virtual CriteriaDetailCollection GetCriteria(ref uint parameterId)
        {
            CriteriaDetails result = GetCriteriaDetails(ref parameterId);
            return new CriteriaDetailCollection(this, result.Criterion, _classOptionsTupla.ClassOptions.PropertyOptions.GetValue(Column), result.Parameters);
        }

        protected abstract CriteriaDetails GetCriteriaDetails(ref uint parameterId);
    }
}