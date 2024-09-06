using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Initializes a new instance of the Criteria class.
    /// </summary>
    /// <param name="classOptionsTupla">ClassOptionsTupla<ColumnAttribute></param>
    /// <param name="formats">Column Attribute</param>
    /// <param name="logicalOperator">Logical Operator </param>
    /// <exception cref="ArgumentNullException"></exception>
    internal abstract class Criteria<T, TProperties>(ClassOptions classOptions, IFormats formats, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : ISearchCriteria
    {
        protected KeyValuePair<string, PropertyOptions>? _keyValue = null;
        protected string _columnName = string.Empty;
        protected string _tableName = string.Empty;
        private readonly Expression<Func<T, TProperties>> _expression = expression;

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
        public ClassOptions ClassOptions => classOptions;

        public Expression<Func<T, TProperties>> Expression => _expression;

        public abstract object Value { get; }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Details of the criteria</returns>
        public virtual CriteriaDetailCollection GetCriteria(ref uint parameterId)
        {
            _keyValue = ExpressionExtension.GetKeyValue(Expression);
            _columnName = _keyValue.Value.Value.FormatColumnName.GetColumnName(Formats, QueryType.Criteria);
            _tableName = _keyValue.Value.Value.FormatColumnName.FormatTableName.GetTableName(Formats);
            CriteriaDetails result = GetCriteriaDetails(ref parameterId);
            return new CriteriaDetailCollection(this, result.Criterion, _keyValue.Value.Value, result.Parameters);
        }

        protected abstract CriteriaDetails GetCriteriaDetails(ref uint parameterId);

        public abstract CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection);
    }
}