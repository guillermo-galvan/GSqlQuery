using FluentSQL.Models;
using System.Runtime.CompilerServices;
using FluentSQL.Extensions;

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

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IStatements Statements => _statements;

        /// <summary>
        /// Create QueryBuilder object 
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
            _statements = statements ?? throw new ArgumentNullException(nameof(selectMember));
            _columns = GetColumnsQuery(selectMember);
            _queryType = queryType;
        }

        private IEnumerable<ColumnAttribute> GetColumnsQuery(IEnumerable<string> selectMember)
        {
            return  (from prop in _options.PropertyOptions
                     join sel in selectMember on prop.PropertyInfo.Name equals sel
                     select prop.ColumnAttribute).ToArray();
        }

        private void ChangeQueryType()
        {
            switch (_queryType)
            {
                case QueryType.Select:
                    _queryType = QueryType.SelectWhere;
                    break;                
                case QueryType.Insert:
                    break;
                case QueryType.Update:
                    break;
                case QueryType.Delete:
                    break;
                default:
                    break;
            }
        }

        private string GetCriteria()
        {
            if (_criteria is null)
            {
                _criteria = _andOr.BuildCriteria();
            }

            return string.Join(" ", _criteria.Select(x => x.Criterion));
        }

        private string GetQuery()
        {
            string tableName = _options.Table.GetTableName(_statements);

            return _queryType switch
            {
                QueryType.Select => string.Format(_statements.Select, string.Join(",", _columns.Select(x => x.GetColumnName(tableName,_statements))), tableName),
                QueryType.SelectWhere => string.Format(_statements.SelectWhere, string.Join(",", _columns.Select(x => x.GetColumnName(tableName,_statements))), tableName, GetCriteria()),
                QueryType.Insert => _statements.Insert,
                QueryType.Update => _statements.Update,
                QueryType.Delete => _statements.DeleteWhere,
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
