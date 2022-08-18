using FluentSQL.Models;
using System.Runtime.CompilerServices;
using FluentSQL.Extensions;
using System.Data.Common;

[assembly: InternalsVisibleTo("FluentSQLTest")]

namespace FluentSQL.Default
{
    /// <summary>
    /// Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class QueryBuilder<T> : IQueryBuilder<T> where T : class, new()
    {
        private readonly ClassOptions _options;
        private readonly IStatements _statements;
        private readonly IEnumerable<ColumnAttribute> _columns;
        private QueryType _queryType;
        private IEnumerable<CriteriaDetail>? _criteria = null;
        private IAndOr<T> _andOr;
        private readonly object _entity;
        private readonly IDictionary<ColumnAttribute, object?> _columnValues;

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IStatements Statements => _statements;

        /// <summary>
        /// Initializes a new instance of the QueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
        /// <param name="queryType">Type of query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBuilder(ClassOptions options, IEnumerable<string> selectMember, IStatements statements, QueryType queryType)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            selectMember = selectMember ?? throw new ArgumentNullException(nameof(selectMember));
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _columns = _options.GetColumnsQuery(selectMember);
            _queryType = queryType;
        }

        /// <summary>
        ///Initializes a new instance of the QueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
        /// <param name="queryType">Type of query</param>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBuilder(ClassOptions options, IEnumerable<string> selectMember, IStatements statements, QueryType queryType, object entity)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            selectMember = selectMember ?? throw new ArgumentNullException(nameof(selectMember));
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columns = _options.GetColumnsQuery(selectMember);
            _queryType = queryType;
        }

        /// <summary>
        /// Initializes a new instance of the QueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>        
        /// <param name="statements">Statements to build the query</param>
        /// <param name="queryType">Type of query</param>
        /// <param name="columnValues">Columns with Values</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBuilder(ClassOptions options, IStatements statements, QueryType queryType, IDictionary<ColumnAttribute, object?> columnValues)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));            
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _columnValues = columnValues ?? throw new ArgumentNullException(nameof(columnValues));
            _columns = columnValues.Keys;
            _queryType = queryType;
        }

        private void ChangeQueryType()
        {
            switch (_queryType)
            {
                case QueryType.Select:
                    _queryType = QueryType.SelectWhere;
                    break;
                case QueryType.Update:
                    _queryType = QueryType.UpdateWhere;
                    break;
                case QueryType.Delete:
                    _queryType = QueryType.DeleteWhere;
                    break;
                default:
                    break;
            }
        }

        private string GetCriteria()
        {
            _criteria ??= _andOr.BuildCriteria();

            return string.Join(" ", _criteria.Select(x => x.QueryPart));
        }

        private (string columnName, ParameterDetail parameterDetail) GetParameterValue(string tableName,ColumnAttribute column)
        {
            PropertyOptions options = _options.PropertyOptions.First(x => x.ColumnAttribute.Name == column.Name);
            return (column.GetColumnName(tableName, _statements), new ParameterDetail($"@PI{options.PropertyInfo.Name}", options.GetValue(_entity)));
        }

        private List<(string columnName, ParameterDetail parameterDetail)> GetValues(string tableName)
        {
            List<(string columnName, ParameterDetail parameterDetail)> values = new();
            foreach (var item in _columns)
            {
                values.Add(GetParameterValue(tableName, item));
            }
            return values;
        }

        private string GetInsertQuery(string tableName)
        {
            if (_entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }
            List<(string columnName, ParameterDetail parameterDetail)> values = GetValues(tableName);
            CriteriaDetail criteriaDetail = new (string.Join(",", values.Select(x => x.parameterDetail.Name)), values.Select(x => x.parameterDetail));
            _criteria = new CriteriaDetail[] { criteriaDetail };
            return string.Format(_statements.Insert, tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart);
        }

        private List<CriteriaDetail> GetUpdateCliterias(string tableName)
        {
            List<CriteriaDetail> criteriaDetails = new();

            foreach (var item in _columnValues)
            {
                PropertyOptions options = _options.PropertyOptions.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = $"@PU{options.PropertyInfo.Name}";
                criteriaDetails.Add(new CriteriaDetail($"{item.Key.GetColumnName(tableName, _statements)}={paramName}",
                    new ParameterDetail[] { new ParameterDetail(paramName, item.Value ?? DBNull.Value) }));
            }
            return criteriaDetails;
        }

        private string GetUpdateQuery(string tableName)
        {
            if (_columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }
            List<CriteriaDetail>  criteria = GetUpdateCliterias(tableName);
            string query = string.Empty;
            _criteria = null;

            if (_queryType == QueryType.Update)
            {
                query = string.Format(_statements.Update, tableName, string.Join(",", criteria.Select(x => x.QueryPart)));
            }
            else
            {
                string where = GetCriteria();                
                query = string.Format(_statements.UpdateWhere, tableName, string.Join(",", criteria.Select(x => x.QueryPart)), where);
                criteria.AddRange(_criteria);
            }

            _criteria = criteria;
            return query;
        }

        private string GetQuery()
        {
            string tableName = _options.Table.GetTableName(_statements);

            return _queryType switch
            {
                QueryType.Select => string.Format(_statements.Select, string.Join(",", _columns.Select(x => x.GetColumnName(tableName,_statements))), tableName),
                QueryType.SelectWhere => string.Format(_statements.SelectWhere, string.Join(",", _columns.Select(x => x.GetColumnName(tableName,_statements))), tableName, GetCriteria()),
                QueryType.Insert =>  GetInsertQuery(tableName),
                QueryType.Update => GetUpdateQuery(tableName),
                QueryType.UpdateWhere => GetUpdateQuery(tableName),
                QueryType.Delete => string.Format(_statements.Delete, tableName),
                QueryType.DeleteWhere => string.Format(_statements.DeleteWhere, tableName, GetCriteria()),
                _ => string.Empty,
            };
        }

        /// <summary>
        /// Build Query
        /// </summary>
        public IQuery<T> Build()
        {
            string query = GetQuery();
            return new Query<T>(_columns, _criteria, _statements, query);
        }

        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        public IWhere<T> Where()
        {
            ChangeQueryType();
            _andOr = new Where<T>(this);
            return (IWhere<T>)_andOr;
        }
    }
}
