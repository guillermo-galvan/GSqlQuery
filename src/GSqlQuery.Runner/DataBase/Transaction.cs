using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public abstract class Transaction<TIConnection, TDbCommand, TDbTransaction, TDbConnection> : ITransaction<TIConnection, TDbTransaction>
        where TIConnection : IConnection
        where TDbTransaction : DbTransaction
        where TDbCommand : DbCommand
        where TDbConnection : DbConnection
    {
        protected readonly TDbTransaction _transaction;
        protected readonly TIConnection _connection;
        protected TransacctionDispose _transacctionDispose;

        IConnection ITransaction.Connection => _connection;

        public IsolationLevel IsolationLevel => _transaction.IsolationLevel;

        TDbTransaction ITransaction<TIConnection, TDbTransaction>.Transaction => _transaction;

        public TIConnection Connection => _connection;

        object ITransaction.Transaction => _transaction;

        public delegate void TransacctionDispose(ITransaction transaction);

        public Transaction(TIConnection connection, TDbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _transacctionDispose += _connection.RemoveTransaction;
        }

        public virtual void Commit()
        {
            _transaction.Commit();
        }

        public virtual Task CommitAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
#if NET5_0_OR_GREATER
            return _transaction.CommitAsync(cancellationToken);
#else
            _transaction.Commit();
            return Task.CompletedTask;
#endif
        }

        public virtual void Rollback()
        {
            _transaction.Rollback();
        }

        public virtual Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
#if NET5_0_OR_GREATER
            return _transaction.RollbackAsync(cancellationToken);
#else
            _transaction.Rollback();
            return Task.CompletedTask;
#endif
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transacctionDispose != null)
                {
                    _transacctionDispose?.Invoke(this);
                    _transacctionDispose -= _connection.RemoveTransaction;
                    _transacctionDispose = null;
                    _transaction?.Dispose();

                }
            }
        }

        ~Transaction()
        {
            Dispose(disposing: false);
        }
    }
}