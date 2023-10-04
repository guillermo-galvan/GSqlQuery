using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria in(IN)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class In<T> : Criteria, ISearchCriteria
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
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="values">Equality value</param>
        public In(TableAttribute table, ColumnAttribute columnAttribute, IEnumerable<T> values) : this(table, columnAttribute, values, null)
        { }

        /// <summary>
        /// Initializes a new instance of the In class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="values">Equality value</param>
        /// <param name="logicalOperator">Logical operator </param>
        /// <exception cref="ArgumentNullException"></exception>
        public In(TableAttribute table, ColumnAttribute columnAttribute, IEnumerable<T> values, string logicalOperator) : base(table, columnAttribute, logicalOperator)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            if (!values.Any())
            {
                throw new IndexOutOfRangeException(nameof(values));
            }
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IFormats formats, IEnumerable<PropertyOptions> propertyOptions)
        {
            string tableName = Table.GetTableName(formats);
            ParameterDetail[] parameters = new ParameterDetail[Values.Count()];
            int count = 0;
            int index = 0;
            ulong ticks = Helpers.GetIdParam();
            var property = Column.GetPropertyOptions(propertyOptions);

            foreach (var item in Values)
            {
                parameters[index++] = new ParameterDetail($"@{ParameterPrefix}{count++}{ticks}", item, property);
            }

            string criterion = $"{Column.GetColumnName(tableName, formats, QueryType.Criteria)} {RelationalOperator} ({string.Join(",", parameters.Select(x => x.Name))})";
            criterion = string.IsNullOrWhiteSpace(LogicalOperator) ? criterion : $"{LogicalOperator} {criterion}";
            return new CriteriaDetail(this, criterion, parameters);
        }
    }
}