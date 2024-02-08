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

            _task = CreteData();
        }

        private Task<CriteriaDetails> CreteData()
        {
            string tableName = TableAttributeExtension.GetTableName(Table, Formats);
            string parameterName = "@" + ParameterPrefix + Helpers.GetIdParam();
            string columName = Formats.GetColumnName(tableName, Column, QueryType.Criteria);

            string criterion = "{0} {1} CONCAT('%', {2}, '%')".Replace("{0}", columName).Replace("{1}", RelationalOperator).Replace("{2}", parameterName);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            PropertyOptions property = ColumnAttributeExtension.GetPropertyOptions(Column, _classOptionsTupla.ClassOptions.PropertyOptions);
            ParameterDetail parameterDetail = new ParameterDetail(parameterName, Value, property);
            return Task.FromResult(new CriteriaDetails(criterion, [parameterDetail]));
        }
    }
}