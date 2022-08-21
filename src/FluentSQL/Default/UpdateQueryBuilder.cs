using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Update query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateQueryBuilder<T> : QueryBuilderWithCriteria<T, UpdateQuery<T>>, IQueryBuilderWithWhere<T, UpdateQuery<T>> where T : class, new()
    {
        private readonly IDictionary<ColumnAttribute, object?> _columnValues;

        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="options">The Query</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
        /// <param name="columnValues">Column values</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(ClassOptions options, IEnumerable<string> selectMember, ConnectionOptions connectionOptions, IDictionary<ColumnAttribute, object?> columnValues) : 
            base(options, selectMember, connectionOptions, QueryType.Update)
        {
            _columnValues = columnValues ?? throw new ArgumentNullException(nameof(columnValues));
            _columns = _columnValues.Keys;
        }

        private List<CriteriaDetail> GetUpdateCliterias()
        {
            List<CriteriaDetail> criteriaDetails = new();

            foreach (var item in _columnValues)
            {
                PropertyOptions options = _options.PropertyOptions.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = $"@PU{options.PropertyInfo.Name}";
                criteriaDetails.Add(new CriteriaDetail($"{item.Key.GetColumnName(_tableName, _connectionOptions.Statements)}={paramName}",
                    new ParameterDetail[] { new ParameterDetail(paramName, item.Value ?? DBNull.Value) }));
            }
            return criteriaDetails;
        }

        protected override string GenerateQuery()
        {
            if (_columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }
            List<CriteriaDetail> criteria = GetUpdateCliterias();
            string query = string.Empty;
            _criteria = null;

            if (_queryType == QueryType.Update)
            {
                query = string.Format(_connectionOptions.Statements.Update, _tableName, string.Join(",", criteria.Select(x => x.QueryPart)));
            }
            else
            {
                string where = GetCriteria();
                query = string.Format(_connectionOptions.Statements.UpdateWhere, _tableName, string.Join(",", criteria.Select(x => x.QueryPart)), where);
#pragma warning disable CS8604 // Possible null reference argument.
                criteria.AddRange(_criteria);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            _criteria = criteria;
            return query;
        }

        /// <summary>
        ///  Build update query
        /// </summary>
        /// <returns>UpdateQuery</returns>
        public UpdateQuery<T> Build()
        {
            return new UpdateQuery<T>(GenerateQuery(), _columns, _criteria, _connectionOptions);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>IWhere</returns>
        public IWhere<T, UpdateQuery<T>> Where()
        {
            ChangeQueryType();
            UpdateWhere<T> selectWhere = new(this);
            _andOr = selectWhere;
            return (IWhere<T, UpdateQuery<T>>)_andOr;
        }
    }
}
