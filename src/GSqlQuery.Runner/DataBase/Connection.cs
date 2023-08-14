using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public abstract class Connection
    {
        protected readonly DbConnection _connection;
        protected bool _disposed = false;
        protected ITransaction _transaction;

        public ConnectionState State => _connection == null ? ConnectionState.Broken : _connection.State;

        public Connection(DbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public virtual void Close()
        {
            _connection.Close();
        }

        public virtual Task CloseAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

#if NET5_0_OR_GREATER
                return _connection.CloseAsync();
#else
            _connection.Close();
            return Task.CompletedTask;
#endif
        }

        public virtual DbCommand GetDbCommand()
        {
            DbCommand result = _connection.CreateCommand();

            if (_transaction != null)
            {
                result.Transaction = _transaction.Transaction;
            }

            return _connection.CreateCommand();
        }

        public virtual Task OpenAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _connection.OpenAsync(cancellationToken);
        }

        public virtual void Open()
        {
            _connection.Open();
        }

        protected ITransaction SetTransaction(ITransaction transaction)
        {
            _transaction = transaction;
            return _transaction;
        }

        public virtual void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void RemoveTransaction(ITransaction transaction)
        {
            _transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }
                _disposed = true;
            }
        }

        ~Connection()
        {
            Dispose(disposing: false);
        }
    }
}