using GSqlQuery.Extensions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GSqlQuery.Runner.Test")]
namespace GSqlQuery.Runner.Queries
{
    internal class SelectQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, SelectQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, SelectQuery<T, TDbConnection>, TDbConnection>, IBuilder<SelectQuery<T, TDbConnection>>
        where T : class, new()
    {
        public SelectQueryBuilder(IEnumerable<string> selectMember, ConnectionOptions<TDbConnection> connectionOptions) :
            base(connectionOptions, QueryType.Select)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember);
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_queryType == QueryType.Select)
            {
                result = string.Format(ConnectionOptions.Statements.Select,
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, ConnectionOptions.Statements))),
                    _tableName);
            }
            else if (_queryType == QueryType.SelectWhere)
            {
                result = string.Format(ConnectionOptions.Statements.SelectWhere,
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, ConnectionOptions.Statements))),
                    _tableName, GetCriteria());
            }

            return result;
        }

        public override SelectQuery<T, TDbConnection> Build()
        {
            return new SelectQuery<T, TDbConnection>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions);
        }

        public override IWhere<T, SelectQuery<T, TDbConnection>> Where()
        {
            ChangeQueryType();
            SelectWhere<T, TDbConnection> selectWhere = new SelectWhere<T, TDbConnection>(this);
            _andOr = selectWhere;
            return (IWhere<T, SelectQuery<T, TDbConnection>>)_andOr;
        }
    }
}
