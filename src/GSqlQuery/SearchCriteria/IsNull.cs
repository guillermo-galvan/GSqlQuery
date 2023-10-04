using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria is null
    /// </summary>
    public class IsNull : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "IS NULL";

        /// <summary>
        /// Initializes a new instance of the IsNull class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        public IsNull(TableAttribute table, ColumnAttribute columnAttribute) : this(table, columnAttribute, null)
        { }

        /// <summary>
        /// Initializes a new instance of the IsNull class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public IsNull(TableAttribute table, ColumnAttribute columnAttribute, string logicalOperator) : base(table, columnAttribute, logicalOperator)
        { }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IFormats formats, IEnumerable<PropertyOptions> propertyOptions)
        {
            string tableName = Table.GetTableName(formats);

            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ?
                $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator}" :
                $"{LogicalOperator} {Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator}";

            return new CriteriaDetail(this, criterion, Enumerable.Empty<ParameterDetail>());
        }
    }
}