using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria in(IN)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class In<T, TProperties> : Criteria<T, TProperties>, ISearchCriteria
    {
        protected virtual string RelationalOperator => "IN";

        protected virtual string ParameterPrefix => "PI";

        /// <summary>
        /// Get Values
        /// </summary>
        public IEnumerable<TProperties> Values { get; }

        public override object Value => Values;

        /// <summary>
        /// Initializes a new instance of the In class.
        /// </summary>
        /// <param name="classOptions">ClassOptions</param>
        /// <param name="formats">Formats</param>
        /// <param name="values">Equality value</param>
        /// <param name="logicalOperator">Logical operator </param>
        /// <param name="expression">Expression</param>
        /// <exception cref="ArgumentNullException"></exception>
        public In(ClassOptions classOptions, IFormats formats, IEnumerable<TProperties> values, string logicalOperator, ref Expression<Func<T, TProperties>> expression)
            : base(classOptions, formats, logicalOperator, ref expression)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            if (!values.Any())
            {
                throw new IndexOutOfRangeException(nameof(values));
            }
        }

        protected override CriteriaDetails GetCriteriaDetails(ref uint parameterId)
        {
            ParameterDetail[] parameters = new ParameterDetail[Values.Count()];
            int count = 0;
            int index = 0;
            string ticks = $"{parameterId++}";

            foreach (TProperties item in Values)
            {
                string parameterName = "@" + ParameterPrefix + count++.ToString() + ticks;
                parameters[index++] = new ParameterDetail(parameterName, item);
            }
            string columnNames = string.Join(",", parameters.Select(x => x.Name));
            string criterion = "{0} {1} ({2})".Replace("{0}", _columnName).Replace("{1}", RelationalOperator).Replace("{2}", columnNames);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            return new CriteriaDetails(criterion, parameters);
        }

        public override CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection)
        {
            if (criteriaDetailCollection.SearchCriteria is In<T, TProperties> parameter)
            {
                uint parameterid = 0;

                return base.GetCriteria(ref parameterid);
            }

            return null;
        }
    }
}