using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Internal;
using FluentSQL.Models;

namespace FluentSQL
{
    public sealed class ContinueExecution<TResult, TDbConnection>
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;
        internal readonly ContinueExecutionResult<TDbConnection> _continueExecutionResult;        

        public ConnectionOptions<TDbConnection> ConnectionOptions => _connectionOptions;

        internal ContinueExecution(ConnectionOptions<TDbConnection> connectionOptions,ContinueExecutionResult<TDbConnection> continueExecution)
        {
            _connectionOptions = connectionOptions;
            _continueExecutionResult = continueExecution;
        }

        public ContinueExecution<TNewResult, TDbConnection> ContinueWith<T, TReturn, TNewResult>
            (Func<ConnectionOptions<TDbConnection>, TResult, IQueryBuilder<T, TReturn, TDbConnection, TNewResult>> exec)
            where T : class, new() where TReturn : IQuery<T, TDbConnection, TNewResult>
        {
            _continueExecutionResult.Add(new ContinueExecutionQueryBuilderResult<T,TReturn,TDbConnection, TResult, TNewResult>(_connectionOptions, exec));
            return new ContinueExecution<TNewResult, TDbConnection>(_connectionOptions, _continueExecutionResult);
        }

        public ContinueExecution<TNewResult, TDbConnection> ContinueWith<T,TReturn, TNewResult>(Func<ConnectionOptions<TDbConnection>, TResult, 
            IAndOr<T, TReturn, TDbConnection, TNewResult>> exec)
            where T : class, new()
            where TReturn : IQuery<T, TDbConnection, TNewResult>
        {
            _continueExecutionResult.Add(new ContinueExecutionIAndOrResult<T, TReturn, TDbConnection, TResult, TNewResult>(_connectionOptions, exec));
            return new ContinueExecution<TNewResult, TDbConnection>(_connectionOptions, _continueExecutionResult);
        }

        public TResult? Start()
        {
            TDbConnection connection = default;
            try
            {
                connection = _connectionOptions.DatabaseManagment.GetConnection();
                TResult? result = Start(connection);
                return result;
            }
            finally 
            {
                if (connection is IDisposable disposable)
                { 
                    disposable.Dispose();
                }
            }
        }

        public TResult? Start(TDbConnection connection)
        {
            TResult? result = (TResult?)_continueExecutionResult.Start(connection);            
            return result;
        }
    }
}
