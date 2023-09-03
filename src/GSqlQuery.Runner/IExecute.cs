using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public interface IExecute<TResult>
    {
        TResult Execute();

        Task<TResult> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IExecute<TResult, TDbConnection> : IExecute<TResult>
    {
        TResult Execute(TDbConnection dbConnection);

        Task<TResult> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default);

        IDatabaseManagement<TDbConnection> DatabaseManagement { get; }
    }
}