﻿using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IExecute<TResult, TDbConnection>
    {
        IDatabaseManagement<TDbConnection> DatabaseManagment { get; }

        TResult Execute();

        TResult Execute(TDbConnection dbConnection);

        Task<TResult> ExecuteAsync(CancellationToken cancellationToken = default);

        Task<TResult> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default);
    }
}
