using System.Data.Common;

namespace FluentSQL.DataBase
{
    public interface ITransaction : IDisposable 
    {
        IConnection Connection { get; }

        DbTransaction Transaction { get; }

        void Commit();

        void Rollback();

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
