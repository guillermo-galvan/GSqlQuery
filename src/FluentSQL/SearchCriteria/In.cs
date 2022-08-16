using FluentSQL.Extensions;

namespace FluentSQL.SearchCriteria
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
        public In(TableAttribute table, ColumnAttribute columnAttribute, IEnumerable<T> values, string? logicalOperator) : base(table, columnAttribute, logicalOperator)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IStatements statements)
        {
            string tableName = Table.GetTableName(statements);
            List<ParameterDetail> parameters = new();
            int count = 0;
            foreach (var item in Values)
            {
                parameters.Add(new ParameterDetail($"@{ParameterPrefix}{count++}{DateTime.Now.Ticks}", item));
            }
            string criterion = $"{tableName}.{Column.GetColumnName(tableName, statements)} {RelationalOperator} ({string.Join(",", parameters.Select(x => x.Name))})";
            criterion = string.IsNullOrWhiteSpace(LogicalOperator) ? criterion : $"{LogicalOperator} {criterion}";
            return new CriteriaDetail(this, criterion, parameters);
        }
    }
}
