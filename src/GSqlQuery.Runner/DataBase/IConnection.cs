using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public interface IConnection : IDisposable
    {
        ConnectionState State { get; }

        DbCommand GetDbCommand();

        void Close();

        Task CloseAsync(CancellationToken cancellationToken = default);

        ITransaction BeginTransaction();

        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task<ITransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);

        void RemoveTransaction(ITransaction transaction);
    }
}