using System.Data;
using System.Data.Common;

namespace GSqlQuery.Runner.DataBase
{
    public interface ITransaction : IDisposable
    {
        IsolationLevel IsolationLevel { get; }

        IConnection Connection { get; }

        DbTransaction Transaction { get; }

        void Commit();

        void Rollback();

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
