using GSqlQuery.Extensions;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(=)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class Equal<T> : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "=";

        protected virtual string ParameterPrefix => "PE";

        /// <summary>
        /// Get equality value
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        public Equal(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, T value) :
            this(classOptionsTupla, formats, value, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public Equal(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, T value, string logicalOperator) :
            base(classOptionsTupla, formats, logicalOperator)
        {
            Value = value;
            _task = CreteData();
        }

        private Task<CriteriaDetails> CreteData()
        {
            string tableName = _classOptionsTupla.ClassOptions.FormatTableName.GetTableName(Formats);
            string parameterName = "@" + ParameterPrefix + Helpers.GetIdParam();
            string columName = _classOptionsTupla.Columns.FormatColumnName.GetColumnName(Formats, QueryType.Criteria);

            string criterion = "{0} {1} {2}".Replace("{0}", columName).Replace("{1}", RelationalOperator).Replace("{2}", parameterName);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }
            PropertyOptions property = GetPropertyOptions(Column, _classOptionsTupla.ClassOptions.PropertyOptions);
            return Task.FromResult(new CriteriaDetails(criterion, [new ParameterDetail(parameterName, Value, property)]));
        }
    }
}