using System;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria BETWEEN
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <remarks>
    /// Initializes a new instance of the Between class.
    /// </remarks>
    /// <param name="classOptions">ClassOptionsTupla</param>
    /// <param name="formats">Formats</param>
    /// <param name="initialValue">Initial value</param>
    /// <param name="logicalOperator">Logical operator</param>
    /// <param name="expression">Expression</param>
    internal class Between<T, TProperties>(ClassOptions classOptions, IFormats formats, TProperties initialValue, TProperties finalValue, string logicalOperator, Expression<Func<T, TProperties>> expression) : Criteria<T, TProperties>(classOptions, formats, logicalOperator, ref expression), ISearchCriteria
    {
        protected virtual string RelationalOperator => "BETWEEN";

        protected virtual string ParameterPrefix => "PB";

        /// <summary>
        /// Get the initial value
        /// </summary>
        public TProperties Initial { get; } = initialValue;

        /// <summary>
        /// Get the final value
        /// </summary>
        public TProperties Final { get; } = finalValue;

        public override object Value => new { Initial, Final };

        protected override CriteriaDetails GetCriteriaDetails(ref uint parameterId)
        {
            string tiks = $"{parameterId++}";
            string parameterName1 = "@{0}1".Replace("{0}", ParameterPrefix) + tiks;
            string parameterName2 = "@{0}2".Replace("{0}", ParameterPrefix) + tiks;

            string criterion = "{0} {1} {2} AND {3}".Replace("{0}", _columnName).Replace("{1}", RelationalOperator)
                                                    .Replace("{2}", parameterName1).Replace("{3}", parameterName2);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            return new CriteriaDetails(criterion, [new ParameterDetail(parameterName1, Initial), new ParameterDetail(parameterName2, Final)]);
        }

        public override CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection)
        {
            if (criteriaDetailCollection.SearchCriteria is not Between<T, TProperties> parameter)
            {
                return null;
            }

            var result = new CriteriaDetailCollection(criteriaDetailCollection.SearchCriteria, criteriaDetailCollection.QueryPart, criteriaDetailCollection.PropertyOptions);

            int count = 0;
            foreach (var item in criteriaDetailCollection.Keys)
            {
                if (count == 0)
                {
                    result[item] = new ParameterDetail(item, Initial);
                }
                else if (count == 1)
                {
                    result[item] = new ParameterDetail(item, Final);
                }
                else
                {
                    break;
                }

                count++;
            }

            return result;
        }
    }
}