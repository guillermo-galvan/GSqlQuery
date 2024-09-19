using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria is null
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the IsNull class.
    /// </remarks>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="logicalOperator">Logical Operator</param>
    /// <param name="expression">Expression</param>
    internal class IsNull<T, TProperties>(ClassOptions classOptions, IFormats formats, string logicalOperator, ref Expression<Func<T, TProperties>> expression) : Criteria<T, TProperties>(classOptions, formats, logicalOperator, ref expression), ISearchCriteria
    {
        public override object Value => null;

        protected virtual string RelationalOperator => "IS NULL";

        public override CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection)
        {
            return criteriaDetailCollection;
        }

        protected override CriteriaDetails GetCriteriaDetails(ref uint parameterId)
        {
            string criterion = "{0} {1}".Replace("{0}", _columnName).Replace("{1}", RelationalOperator);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            return new CriteriaDetails(criterion, []);
        }
    }
}