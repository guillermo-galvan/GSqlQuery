using GSqlQuery.Extensions;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class Between<T> : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "BETWEEN";

        protected virtual string ParameterPrefix => "PB";

        /// <summary>
        /// Get the initial value
        /// </summary>
        public T Initial { get; }

        /// <summary>
        /// Get the final value
        /// </summary>
        public T Final { get; }

        /// <summary>
        /// Initializes a new instance of the Between class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="finalValue">Logical operator</param>
        public Between(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T initialValue, T finalValue) :
            this(classOptionsTupla, formats, initialValue, finalValue, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Between class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="initialValue">Initial value</param>
        /// <param name="logicalOperator">Logical operator</param>
        public Between(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, T initialValue, T finalValue, string logicalOperator) :
            base(classOptionsTupla, formats, logicalOperator)
        {
            Initial = initialValue;
            Final = finalValue;
            _task = CreteData();
        }

        private Task<CriteriaDetails> CreteData()
        {
            string tableName = TableAttributeExtension.GetTableName(Table, Formats);
            string tiks = Helpers.GetIdParam().ToString();
            string parameterName1 = "@{0}1".Replace("{0}", ParameterPrefix) + tiks;
            string parameterName2 = "@{0}2".Replace("{0}", ParameterPrefix) + tiks;
            string columName = Formats.GetColumnName(tableName, Column, QueryType.Criteria);

            string criterion = "{0} {1} {2} AND {3}".Replace("{0}", columName).Replace("{1}", RelationalOperator)
                                                    .Replace("{2}", parameterName1).Replace("{3}", parameterName2);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            PropertyOptions property = GetPropertyOptions(Column, _classOptionsTupla.ClassOptions.PropertyOptions);

            return Task.FromResult(new CriteriaDetails(criterion, [new ParameterDetail(parameterName1, Initial, property), new ParameterDetail(parameterName2, Final, property)]));
        }
    }
}