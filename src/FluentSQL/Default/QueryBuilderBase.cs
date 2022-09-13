using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    public abstract class QueryBuilderBase
    {
        private readonly IStatements _statements;
        protected QueryType _queryType;
        protected readonly string _tableName;

        public IEnumerable<PropertyOptions> Columns { get; protected set; }

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        public IStatements Statements => _statements;

        public QueryBuilderBase(string tableName, IEnumerable<PropertyOptions> columns, IStatements statements, QueryType queryType)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            Columns = columns ?? throw new ArgumentNullException(nameof(columns));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName)); ;
            _queryType = queryType;
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
                    _queryType = QueryType.Custom;
                    break;
            }
        }

        protected abstract string GenerateQuery();
    }
}
