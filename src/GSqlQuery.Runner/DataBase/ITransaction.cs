using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public interface ITransaction : IDisposable
    {
        IsolationLevel IsolationLevel { get; }

        IConnection Connection { get; }

        object Transaction { get; }

        void Commit();

        void Rollback();

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }

    public interface ITransaction<TIConnection, TDbTransaction> : ITransaction, IDisposable
        where TIConnection : IConnection
        where TDbTransaction : DbTransaction
    {
        new TDbTransaction Transaction { get; }

        new TIConnection Connection { get; }
    }
}