using FluentSQL.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FluentSQLTest")]

namespace FluentSQL.Default
{
    /// <summary>
    /// Query Builder
    /// </summary>
    internal class QueryBuilder : IQueryBuilder
    {
        private readonly ClassOptions _options;
        private readonly IStatements _statements;
        private IEnumerable<ColumnAttribute> _columns;
        private readonly QueryType _queryType;

        /// <summary>
        /// Get Columns of the query
        /// </summary>
        public IEnumerable<ColumnAttribute> Columns => _columns;

        /// <summary>
        /// Create QueryBuilder object with default declarations
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
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

        private string GetTable()
        {
            return string.IsNullOrWhiteSpace(_options.Table.Scheme) ? string.Format(_statements.Format, _options.Table.TableName) :
                   $"{string.Format(_statements.Format, _options.Table.Scheme)}.{string.Format(_statements.Format, _options.Table.TableName)}";
        }

        /// <summary>
        /// Build Query
        /// </summary>
        public string Build()
        {
            return _queryType switch
            {
                QueryType.Select => string.Format(_statements.Select, string.Join(",", _columns.Select(x => string.Format(_statements.Format, x.Name))), GetTable()),
                QueryType.SelectWhere => string.Empty,
                QueryType.Insert => string.Empty,
                QueryType.Update => string.Empty,
                QueryType.Delete => string.Empty,
                _ => string.Empty,
            };
        }
    }
}
