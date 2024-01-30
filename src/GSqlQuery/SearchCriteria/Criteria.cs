using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
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
        protected readonly ClassOptionsTupla<ColumnAttribute> _classOptionsTupla = classOptionsTupla ?? throw new ArgumentNullException(nameof(classOptionsTupla));

        protected Task<CriteriaDetails> _task;

        /// <summary>
        /// Get Column
        /// </summary>
        public ColumnAttribute Column { get; } = classOptionsTupla.MemberInfo;

        /// <summary>
        /// Get Table
        /// </summary>
        public TableAttribute Table { get; } = classOptionsTupla.ClassOptions.Table;

        /// <summary>
        /// Get logical operator
        /// </summary>
        public string LogicalOperator { get; } = logicalOperator;

        public IFormats Formats { get; } = formats ?? throw new ArgumentNullException(nameof(formats));

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Details of the criteria</returns>
        public virtual CriteriaDetail GetCriteria(IFormats formats, IEnumerable<PropertyOptions> propertyOptions)
        {
            _task.Wait();
            var result = _task.Result;
            return new CriteriaDetail(this, result.Criterion, result.Parameters);
        }
    }
}