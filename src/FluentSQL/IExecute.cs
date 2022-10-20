using FluentSQL.Models;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IExecute<TResult, TDbConnection>
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagment { get; }

        TResult Exec();

        TResult Exec(TDbConnection dbConnection);
    }
}
