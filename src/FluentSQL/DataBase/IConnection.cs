using System.Data;
using System.Data.Common;

namespace FluentSQL.DataBase
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
