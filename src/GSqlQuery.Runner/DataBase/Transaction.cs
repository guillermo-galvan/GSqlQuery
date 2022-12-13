using System.Data;
using System.Data.Common;

namespace GSqlQuery.Runner.DataBase
{
    public abstract class Transaction : ITransaction
    {
        protected bool _disposed;
        protected readonly DbTransaction _transaction;
        protected readonly IConnection _connection;
        protected TransacctionDispose? _transacctionDispose;

        IConnection ITransaction.Connection => _connection;

        DbTransaction ITransaction.Transaction => _transaction;

        public IsolationLevel IsolationLevel => _transaction.IsolationLevel;


        public delegate void TransacctionDispose(ITransaction transaction);

        public Transaction(IConnection connection, DbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _transacctionDispose += _connection.RemoveTransaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _transaction.CommitAsync(cancellationToken);
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _transaction.RollbackAsync(cancellationToken);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transacctionDispose?.Invoke(this);
                    _transacctionDispose -= _connection!.RemoveTransaction!;
                    _transacctionDispose = null;
                    _transaction.Dispose();

                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~Transaction()
        {
            Dispose(disposing: false);
        }
    }
}
