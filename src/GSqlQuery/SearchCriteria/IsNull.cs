using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria is null
    /// </summary>
    internal class IsNull : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "IS NULL";

        /// <summary>
        /// Initializes a new instance of the IsNull class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        public IsNull(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats) :
            this(classOptionsTupla, formats, null)
        { }

        /// <summary>
        /// Initializes a new instance of the IsNull class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public IsNull(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, string logicalOperator) :
            base(classOptionsTupla, formats, logicalOperator)
        {
            string tableName = Table.GetTableName(formats);

            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator}" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator}";

            _task =  Task.FromResult(new CriteriaDetails(criterion, []));
        }
    }
}