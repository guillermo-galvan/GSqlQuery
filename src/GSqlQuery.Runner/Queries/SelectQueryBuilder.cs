using GSqlQuery.Extensions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GSqlQuery.Runner.Test, PublicKey=0024000004800000940000000602000000240000525341310004000001000100913cebd9950f6fcb7fb913297422ef8f3cbdec249d3bbba88346b2045500eeda9546b5fd977bc95be5efb2ca6a8f15a2907dc1bab80d177d2e43b77db77befe6ce26b647e89871a9fede8174dc504ac3322cf5952141cf5fbbdf789fc074bcced5cdc939120d2f67ac483495a97d4df9d3a5fe13f76e40840ee0d70b2dda4b9c")]
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
