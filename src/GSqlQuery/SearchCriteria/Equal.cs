using System;
using System.Linq;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(=)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <remarks>
    /// Initializes a new instance of the Equal class.
    /// </remarks>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Equality value</param>
    /// <param name="logicalOperator">Logical Operator</param>
    /// <param name="expression">Expression</param>
    internal class Equal<T, TProperties>(ClassOptions classOptions, IFormats formats, TProperties value, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : Criteria<T, TProperties>(classOptions, formats, logicalOperator, ref expression), ISearchCriteria
    {
        protected virtual string RelationalOperator => "=";

        protected virtual string ParameterPrefix => "PE";

        /// <summary>
        /// Get equality value
        /// </summary>
        public TProperties Data { get; } = value;

        public override object Value => Data;

        protected override CriteriaDetails GetCriteriaDetails(ref uint parameterId)
        {
            string parameterName = "@" + ParameterPrefix + parameterId++;

            string criterion = "{0} {1} {2}".Replace("{0}", _columnName).Replace("{1}", RelationalOperator).Replace("{2}", parameterName);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            return new CriteriaDetails(criterion, [new ParameterDetail(parameterName, Data)]);
        }

        public override CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection)
        {
            if (criteriaDetailCollection.SearchCriteria is Equal<T, TProperties> parameter)
            {
                CriteriaDetailCollection result = new CriteriaDetailCollection(criteriaDetailCollection.SearchCriteria, criteriaDetailCollection.QueryPart, criteriaDetailCollection.PropertyOptions);
                string nameColumn = criteriaDetailCollection.Keys.First();

                result[nameColumn] = new ParameterDetail(nameColumn, Data);
                return result;
            }

            return null;
        }
    }
}