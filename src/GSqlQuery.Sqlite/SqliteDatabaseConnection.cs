using GSqlQuery.Runner;
using Microsoft.Data.Sqlite;
using System.Data;

namespace GSqlQuery.Sqlite
{
    public class SqliteDatabaseConnection : Connection, IConnection
    {
        public SqliteDatabaseConnection(string connectionString) : base(new SqliteConnection(connectionString))
        { }

        public SqliteDatabaseTransaction BeginTransaction()
        {
            return (SqliteDatabaseTransaction)SetTransaction(new SqliteDatabaseTransaction(this, ((SqliteConnection)_connection).BeginTransaction()));
        }

        public SqliteDatabaseTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return (SqliteDatabaseTransaction)SetTransaction(new SqliteDatabaseTransaction(this, ((SqliteConnection)_connection).BeginTransaction(isolationLevel)));
        }

        public async Task<SqliteDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (SqliteDatabaseTransaction)SetTransaction(new SqliteDatabaseTransaction(this, await ((SqliteConnection)_connection).BeginTransactionAsync(cancellationToken)));
        }

        public async Task<SqliteDatabaseTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return
                (SqliteDatabaseTransaction)SetTransaction(new SqliteDatabaseTransaction(this,
                await ((SqliteConnection)_connection).BeginTransactionAsync(isolationLevel, cancellationToken)));
        }

        ITransaction IConnection.BeginTransaction() => BeginTransaction();

        ITransaction IConnection.BeginTransaction(IsolationLevel isolationLevel) => BeginTransaction(isolationLevel);

        async Task<ITransaction> IConnection.BeginTransactionAsync(CancellationToken cancellationToken = default) => await BeginTransactionAsync(cancellationToken);

        async Task<ITransaction> IConnection.BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default) =>
            await BeginTransactionAsync(isolationLevel, cancellationToken);

        ~SqliteDatabaseConnection()
        {
            Dispose(disposing: false);
        }
    }
}