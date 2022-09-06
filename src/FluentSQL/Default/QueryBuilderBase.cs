using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    internal abstract class QueryBuilderBase
    {
        protected readonly ClassOptions _options;
        private readonly IStatements _statements;
        protected IEnumerable<ColumnAttribute> _columns;
        protected QueryType _queryType;
        protected readonly string _tableName;

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IStatements Statements => _statements;

        public QueryBuilderBase(ClassOptions options, IEnumerable<string> selectMember, IStatements statements, QueryType queryType)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _columns = _options.GetColumnsQuery(selectMember);
            _queryType = queryType;
            _tableName = _options.Table.GetTableName(_statements);
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
