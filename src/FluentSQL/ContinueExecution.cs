using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Internal;

namespace FluentSQL
{
    public sealed class ContinueExecution<TResult, TDbConnection>
    {
        private readonly IStatements _statements;
        internal readonly ContinueExecutionResult<TDbConnection> _continueExecutionResult;
        private readonly IDatabaseManagement<TDbConnection> _databaseManagement;

        public IDatabaseManagement<TDbConnection> DatabaseManagement => _databaseManagement;

        internal ContinueExecution(IStatements statements, ContinueExecutionResult<TDbConnection> continueExecution, 
            IDatabaseManagement<TDbConnection> databaseManagement)
        {
            _statements = statements;
            _continueExecutionResult = continueExecution;
            _databaseManagement = databaseManagement;
        }

        public ContinueExecution<TNewResult, TDbConnection> ContinueWith<TNewResult>(Func<IStatements, TResult, ISetDatabaseManagement<TNewResult>> exec)
        {
            _continueExecutionResult.Add(new ContinueExecutionResult<TDbConnection, TResult, TNewResult>(_statements, exec, _databaseManagement));
            return new ContinueExecution<TNewResult, TDbConnection>(_statements, _continueExecutionResult, _databaseManagement);
        }

        public ContinueExecution<TTypeResult, TDbConnection> ContinueWith<TReturn, TTypeResult>(Func<IStatements, TResult, IBuilder<TReturn>> exec)
            where TReturn : ISetDatabaseManagement<TTypeResult> where TTypeResult : struct
        {
            _continueExecutionResult.Add(new ContinueExecutionResult2<TDbConnection, TResult, TReturn, TTypeResult>(_statements, exec, _databaseManagement));
            return new ContinueExecution<TTypeResult, TDbConnection>(_statements, _continueExecutionResult, _databaseManagement);
        }

        public ContinueExecution<IEnumerable<T>, TDbConnection> ContinueWith<T>(Func<IStatements, TResult, IBuilder<SelectQuery<T>>> exec)
            where T : class, new()
        {
            _continueExecutionResult.Add(new ContinueExecutionResult3<TDbConnection, TResult, T>(_statements, exec, _databaseManagement));
            return new ContinueExecution<IEnumerable<T>, TDbConnection>(_statements, _continueExecutionResult, _databaseManagement);
        }

        public TResult? Start()
        {
            TDbConnection connection = default;
            try
            {
                connection = _databaseManagement.GetConnection();
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
