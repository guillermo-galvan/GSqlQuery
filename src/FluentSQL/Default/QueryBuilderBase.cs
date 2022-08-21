using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    internal abstract class QueryBuilderBase
    {
        protected readonly ClassOptions _options;
        protected readonly ConnectionOptions _connectionOptions;
        protected IEnumerable<ColumnAttribute> _columns;
        protected QueryType _queryType;
        protected readonly string _tableName;

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public ConnectionOptions ConnectionOptions => _connectionOptions;

        public QueryBuilderBase(ClassOptions options, IEnumerable<string> selectMember, ConnectionOptions connectionOptions, QueryType queryType)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            _columns = _options.GetColumnsQuery(selectMember);
            _queryType = queryType;
            _tableName = _options.Table.GetTableName(_connectionOptions.Statements);
        }

        protected void ChangeQueryType()
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

        protected abstract string GenerateQuery();
    }
}
