using GSqlQuery.Extensions;
using GSqlQuery.Runner;
using GSqlQuery.Runner.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public sealed class UpdateQuery<T, TDbConnection> : Query<T, TDbConnection, int>, IQueryRunner<T, TDbConnection, int>,
        IExecuteDatabaseManagement<int, TDbConnection> where T : class, new()
    {
        internal UpdateQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        {
        }

        public override int Execute()
        {
            return DatabaseManagement.ExecuteNonQuery(this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public override int Execute(TDbConnection dbConnection)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteNonQuery(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return DatabaseManagement.ExecuteNonQueryAsync(this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }

        public override Task<int> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteNonQueryAsync(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }
    }
}
