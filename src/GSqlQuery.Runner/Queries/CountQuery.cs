using GSqlQuery.Extensions;
using GSqlQuery.Runner;
using GSqlQuery.Runner.Extensions;

namespace GSqlQuery
{
    public class CountQuery<T, TDbConnection> : Query<T, TDbConnection, int>, IQuery<T, TDbConnection, int>,
        IExecute<int, TDbConnection> where T : class, new()
    {
        public CountQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(text, columns, criteria, connectionOptions)
        {
        }

        public override int Execute()
        {
            return DatabaseManagment.ExecuteScalar<int>(this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override int Execute(TDbConnection dbConnection)
        {
            dbConnection!.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagment.ExecuteScalar<int>(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return DatabaseManagment.ExecuteScalarAsync<int>(this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
        }

        public override Task<int> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection!.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagment.ExecuteScalarAsync<int>(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
        }
    }
}
