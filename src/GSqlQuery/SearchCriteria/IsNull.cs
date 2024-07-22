using GSqlQuery.Extensions;
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
        public IsNull(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats) :
            this(classOptionsTupla, formats, null)
        { }

        /// <summary>
        /// Initializes a new instance of the IsNull class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public IsNull(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, string logicalOperator) :
            base(classOptionsTupla, formats, logicalOperator)
        {
            _task = CreteData();
        }

        private Task<CriteriaDetails> CreteData()
        {
            string tableName = _classOptionsTupla.ClassOptions.FormatTableName.GetTableName(Formats);
            string columName = _classOptionsTupla.Columns.FormatColumnName.GetColumnName(Formats, QueryType.Criteria);
            string criterion = "{0} {1}".Replace("{0}", columName).Replace("{1}", RelationalOperator);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            return Task.FromResult(new CriteriaDetails(criterion, []));
        }
    }
}