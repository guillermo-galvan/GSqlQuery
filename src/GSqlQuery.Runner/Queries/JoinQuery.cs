using GSqlQuery.Extensions;
using GSqlQuery.Runner.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class JoinQuery<T, TDbConnection> : JoinQuery<T>, IExecute<IEnumerable<T>, TDbConnection>
        where T : class, new()
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        internal JoinQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(text, columns, criteria, connectionOptions.Statements)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        public IEnumerable<T> Execute()
        {
            return DatabaseManagement.ExecuteReader(this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteReader(dbConnection, this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagement));
        }

        public Task<IEnumerable<T>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return DatabaseManagement.ExecuteReaderAsync(this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }

        public Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));
            return DatabaseManagement.ExecuteReaderAsync(dbConnection, this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
        }
    }
}