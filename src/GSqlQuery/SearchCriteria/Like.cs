using System;
using System.Linq;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria like (LIKE)
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the Like class.
    /// </remarks>
    /// <param name="classOptions">ClassOptions</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Equality value</param>
    /// <param name="logicalOperator">Logical Operator</param>
    /// <param name="dynamicQuery">DynamicQuery</param>
    internal class Like<T,TProperties>(ClassOptions classOptions, IFormats formats, string value, string logicalOperator,ref Expression<Func<T, TProperties>> expression) : Criteria<T, TProperties>(classOptions, formats, logicalOperator, ref expression)
    {
        protected virtual string RelationalOperator => "LIKE";

        protected virtual string ParameterPrefix => "PL";

        public string Data => value;

        public override object Value => Data;

        protected override CriteriaDetails GetCriteriaDetails(ref uint parameterId)
        {
            string parameterName = "@" + ParameterPrefix + parameterId++;
            string criterion = "{0} {1} CONCAT('%', {2}, '%')".Replace("{0}", _columnName).Replace("{1}", RelationalOperator).Replace("{2}", parameterName);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }
            ParameterDetail parameterDetail = new ParameterDetail(parameterName, Data);
            return new CriteriaDetails(criterion, [parameterDetail]);
        }

        public override CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection)
        {
            if (criteriaDetailCollection.SearchCriteria is Like<T, TProperties> parameter)
            {
                var result = new CriteriaDetailCollection(criteriaDetailCollection.SearchCriteria, criteriaDetailCollection.QueryPart, criteriaDetailCollection.PropertyOptions);
                var nameColumn = criteriaDetailCollection.Keys.First();

                result[nameColumn] = new ParameterDetail(nameColumn, Data);
                return result;
            }

            return null;
        }
    }
}