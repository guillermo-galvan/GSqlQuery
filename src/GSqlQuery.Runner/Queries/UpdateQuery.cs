using GSqlQuery.Extensions;
using GSqlQuery.Runner.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class UpdateQuery<T, TDbConnection> : UpdateQuery<T>, IExecute<int, TDbConnection>
        where T : class, new()
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        internal UpdateQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions) :
            base(text, columns, criteria, connectionOptions.Statements)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }
        public int Execute()
        {
            return DatabaseManagement.ExecuteNonQuery(this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public int Execute(TDbConnection dbConnection)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteNonQuery(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return DatabaseManagement.ExecuteNonQueryAsync(this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteNonQueryAsync(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }
    }
}