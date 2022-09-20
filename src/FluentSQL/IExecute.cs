using FluentSQL.Models;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IExecute<TResult, TDbConnection>
    {
        ConnectionOptions<TDbConnection> ConnectionOptions { get; }

        TResult Exec();

        TResult Exec(TDbConnection dbConnection);
    }
}
