using FluentSQL.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FluentSQL.SQLServer
{
    public class SqlServerDatabaseConnection : Connection, IConnection
    {
        public SqlServerDatabaseConnection(string connectionString) : base(new SqlConnection(connectionString))
        { }

        public SqlServerDatabaseTransaction BeginTransaction()
        {
            return (SqlServerDatabaseTransaction)SetTransaction(new SqlServerDatabaseTransaction(this, ((SqlConnection)_connection).BeginTransaction()));
        }

        public SqlServerDatabaseTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return (SqlServerDatabaseTransaction)SetTransaction(new SqlServerDatabaseTransaction(this, ((SqlConnection)_connection).BeginTransaction(isolationLevel)));
        }

        public async Task<SqlServerDatabaseTransaction> BeginTransactionAsync()
        {
            return (SqlServerDatabaseTransaction)SetTransaction(new SqlServerDatabaseTransaction(this, await ((SqlConnection)_connection).BeginTransactionAsync()));
        }

        public async Task<SqlServerDatabaseTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return
                (SqlServerDatabaseTransaction)SetTransaction(new SqlServerDatabaseTransaction(this,
                await ((SqlConnection)_connection).BeginTransactionAsync(isolationLevel, cancellationToken)));
        }

        ITransaction IConnection.BeginTransaction() => BeginTransaction();

        ITransaction IConnection.BeginTransaction(IsolationLevel isolationLevel) => BeginTransaction(isolationLevel);

        async Task<ITransaction> IConnection.BeginTransactionAsync() => await BeginTransactionAsync();

        async Task<ITransaction> IConnection.BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default) =>
            await BeginTransactionAsync(isolationLevel, cancellationToken);

        ~SqlServerDatabaseConnection()
        {
            Dispose(disposing: false);
        }
    }
}
