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
        public In(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, IEnumerable<T> values) : this(classOptionsTupla, formats, values, null)
        { }

        /// <summary>
        /// Initializes a new instance of the In class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="values">Equality value</param>
        /// <param name="logicalOperator">Logical operator </param>
        /// <exception cref="ArgumentNullException"></exception>
        public In(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, IEnumerable<T> values, string logicalOperator) 
            : base(classOptionsTupla, formats, logicalOperator)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            if (!values.Any())
            {
                throw new IndexOutOfRangeException(nameof(values));
            }

            _task = Task.Factory.StartNew(() =>
            {

                string tableName = Table.GetTableName(formats);
                ParameterDetail[] parameters = new ParameterDetail[Values.Count()];
                int count = 0;
                int index = 0;
                int ticks = Helpers.GetIdParam();
                var property = Column.GetPropertyOptions(classOptionsTupla.ClassOptions.PropertyOptions);

                foreach (var item in Values)
                {
                    parameters[index++] = new ParameterDetail($"@{ParameterPrefix}{count++}{ticks}", item, property);
                }

                string criterion = $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} ({string.Join(",", parameters.Select(x => x.Name))})";
                criterion = string.IsNullOrWhiteSpace(LogicalOperator) ? criterion : $"{LogicalOperator} {criterion}";

                return new CriteriaDetails(criterion, parameters);
            });
        }
    }
}