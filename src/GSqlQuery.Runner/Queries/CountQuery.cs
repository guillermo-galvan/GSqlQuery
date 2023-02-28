using GSqlQuery.Extensions;
using GSqlQuery.Runner;
using GSqlQuery.Runner.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public sealed class CountQuery<T, TDbConnection> : Query<T, TDbConnection, int>, IQueryRunner<T, TDbConnection, int>,
        IExecuteDatabaseManagement<int, TDbConnection> where T : class, new()
    {
        internal CountQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(text, columns, criteria, connectionOptions)
        {
        }

        public override int Execute()
        {
            return DatabaseManagement.ExecuteScalar<int>(this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public override int Execute(TDbConnection dbConnection)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteScalar<int>(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return DatabaseManagement.ExecuteScalarAsync<int>(this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }

        public override Task<int> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteScalarAsync<int>(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }
    }
}
