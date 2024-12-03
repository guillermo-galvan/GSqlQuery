using GSqlQuery.Runner.DataBase;
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public abstract class Connection<TItransaccion, TDbConnection, TDbTransaction, TDbCommand>(TDbConnection connection) : IConnection<TItransaccion, TDbCommand>, IDisposable
        where TItransaccion : ITransaction
        where TDbConnection : DbConnection
        where TDbTransaction : DbTransaction
        where TDbCommand : DbCommand
    {
        private SafeConnectionHandler _safeConnectionHandler = new SafeConnectionHandler(connection);
        protected TDbConnection _connection = connection;
        protected TItransaccion _transaction;

        public ConnectionState State => _connection == null ? ConnectionState.Broken : _connection.State;

        public virtual TDbCommand GetDbCommand()
        {
            TDbCommand result = (TDbCommand)_connection.CreateCommand();

            if (_transaction != null)
            {
                result.Transaction = (TDbTransaction)_transaction.Transaction;
            }

            return result;
        }

        object IConnection.GetDbCommand()
        {
            return GetDbCommand();
        }

        public virtual void Close()
        {
            _connection?.Close();
        }

        public virtual Task CloseAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

#if NET5_0_OR_GREATER
            return _connection.CloseAsync();
#else
            _connection?.Close();
            return Task.CompletedTask;
#endif
        }

        public virtual Task OpenAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _connection.OpenAsync(cancellationToken);
        }

        public virtual void Open()
        {
            _connection?.Open();
        }

        protected TItransaccion SetTransaction(TItransaccion transaction)
        {
            if (_safeConnectionHandler != null)
            {
                _safeConnectionHandler.Transaction = transaction;
                _transaction = transaction;
                return _transaction;
            }

            throw new InvalidOperationException("The connection is disposed");
        }

        public virtual void RemoveTransaction(ITransaction transaction)
        {
            if (_safeConnectionHandler == null)
            {
                throw new InvalidOperationException("The connection is disposed");
            }

            if (transaction.Equals(_transaction))
            {
                _safeConnectionHandler.Transaction = null;
                _transaction = default;
            }
        }

        public virtual void RemoveTransaction(TItransaccion transaction)
        {
            RemoveTransaction((ITransaction)transaction);
        }

        public abstract TItransaccion BeginTransaction();

        public abstract TItransaccion BeginTransaction(IsolationLevel isolationLevel);

        public abstract Task<TItransaccion> BeginTransactionAsync(CancellationToken cancellationToken = default);

        public abstract Task<TItransaccion> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);

        ITransaction IConnection.BeginTransaction()
        {
            return BeginTransaction();
        }

        ITransaction IConnection.BeginTransaction(IsolationLevel isolationLevel)
        {
            return BeginTransaction(isolationLevel);
        }

        async Task<ITransaction> IConnection.BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await BeginTransactionAsync(cancellationToken);
        }

        async Task<ITransaction> IConnection.BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            return await BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public virtual void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _safeConnectionHandler?.Dispose();
                _safeConnectionHandler = null;
            }
        }

        ~Connection()
        {
            Dispose(disposing: false);
        }
    }
}
