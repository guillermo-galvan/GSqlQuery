using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria in(IN)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class In<T> : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "IN";

        protected virtual string ParameterPrefix => "PI";

        /// <summary>
        /// Get Values
        /// </summary>
        public IEnumerable<T> Values { get; }

        /// <summary>
        /// Initializes a new instance of the In class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="values">Equality value</param>
        public In(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, IEnumerable<T> values) : this(classOptionsTupla, formats, values, null)
        { }

        /// <summary>
        /// Initializes a new instance of the In class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="values">Equality value</param>
        /// <param name="logicalOperator">Logical operator </param>
        /// <exception cref="ArgumentNullException"></exception>
        public In(ClassOptionsTupla<PropertyOptions> classOptionsTupla, IFormats formats, IEnumerable<T> values, string logicalOperator)
            : base(classOptionsTupla, formats, logicalOperator)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            if (!values.Any())
            {
                throw new IndexOutOfRangeException(nameof(values));
            }
        }

        protected override CriteriaDetails GetCriteriaDetails(ref uint parameterId)
        {
            string tableName = _classOptionsTupla.ClassOptions.FormatTableName.GetTableName(Formats);
            ParameterDetail[] parameters = new ParameterDetail[Values.Count()];
            int count = 0;
            int index = 0;
            string ticks = $"{parameterId++}";
            string columName = _classOptionsTupla.Columns.FormatColumnName.GetColumnName(Formats, QueryType.Criteria);

            foreach (T item in Values)
            {
                string parameterName = "@" + ParameterPrefix + count++.ToString() + ticks;
                parameters[index++] = new ParameterDetail(parameterName, item);
            }
            string columnNames = string.Join(",", parameters.Select(x => x.Name));
            string criterion = "{0} {1} ({2})".Replace("{0}", columName).Replace("{1}", RelationalOperator).Replace("{2}", columnNames);

            if (!string.IsNullOrWhiteSpace(LogicalOperator))
            {
                criterion = "{0} {1}".Replace("{0}", LogicalOperator).Replace("{1}", criterion);
            }

            return new CriteriaDetails(criterion, parameters);
        }
    }
}