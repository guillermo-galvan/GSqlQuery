﻿using GSqlQuery.Extensions;
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
        public Like(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, string value) :
            this(classOptionsTupla, formats, value, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Like class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public Like(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, string value, string logicalOperator) :
            base(classOptionsTupla, formats, logicalOperator)
        {
            Value = value;
        }

        protected override CriteriaDetails GetCriteriaDetails(ref uint parameterId)
        {
            string tableName = _classOptionsTupla.ClassOptions.FormatTableName.GetTableName(Formats);
            string parameterName = "@" + ParameterPrefix + parameterId++;
            string columName = _classOptionsTupla.Columns.FormatColumnName.GetColumnName(Formats, QueryType.Criteria);

            string criterion = "{0} {1} CONCAT('%', {2}, '%')".Replace("{0}", columName).Replace("{1}", RelationalOperator).Replace("{2}", parameterName);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }
            ParameterDetail parameterDetail = new ParameterDetail(parameterName, Value);
            return new CriteriaDetails(criterion, [parameterDetail]);
        }
    }
}