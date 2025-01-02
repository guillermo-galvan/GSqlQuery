using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GSqlQuery.Runner.DataBase
{
    internal sealed class SafeTransactionHandler : SafeHandle
    {
        public delegate void TransacctionDispose(ITransaction transaction);

        private DbTransaction _transaction;
        private TransacctionDispose _transacctionDispose;
        private IConnection _connection;
        private ITransaction _iTransaction;

        public override bool IsInvalid => _transaction == null;

        public SafeTransactionHandler(IConnection connection, DbTransaction transaction, ITransaction iTransaction) : base(IntPtr.Zero, true)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            _iTransaction = iTransaction ?? throw new ArgumentNullException(nameof(iTransaction));
            _transacctionDispose += _connection.RemoveTransaction;
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                _transacctionDispose?.Invoke(_iTransaction);
                _transacctionDispose -= _connection.RemoveTransaction;
                _transaction?.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error releasing handle: {ex.Message}");
            }
            finally
            {
                _transacctionDispose = null;
                _transaction = null;
                _connection = null;
                _iTransaction = null;
            }

            return true;
        }
    }
}