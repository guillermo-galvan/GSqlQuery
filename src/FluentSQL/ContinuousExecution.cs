using FluentSQL.Default;
using FluentSQL.Internal;
using System.Security.Principal;

namespace FluentSQL
{
    public sealed class ContinuousExecution<TDbConnection>
    {
        private readonly IStatements _statements;
        private readonly IDatabaseManagement<TDbConnection> _databaseManagement;

        public ContinuousExecution(IStatements statements, IDatabaseManagement<TDbConnection> databaseManagement)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _databaseManagement = databaseManagement ?? throw new ArgumentNullException(nameof(databaseManagement));
        }

        public ContinueExecution<TResult, TDbConnection> New<TResult>(
            Func<IStatements, ISetDatabaseManagement<TResult>> query)
        {
            return new ContinueExecution<TResult, TDbConnection>(_statements,
                new ContinueExecutionResult<TDbConnection, TResult>(_statements, query, _databaseManagement), _databaseManagement);
        }

        public ContinueExecution<IEnumerable<T>, TDbConnection> New<T>(Func<IStatements, IBuilder<SelectQuery<T>>> exec)
            where T : class, new()
        {
            return new ContinueExecution<IEnumerable<T>, TDbConnection>(_statements, 
                new ContinueExecutionResult3<TDbConnection, T>(_statements, exec, _databaseManagement), _databaseManagement);
        }

        public ContinueExecution<TTypeResult, TDbConnection> New<TReturn,TTypeResult>(Func<IStatements, IBuilder<TReturn>> exec)
            where TReturn : ISetDatabaseManagement<TTypeResult> where TTypeResult : struct
        {
            return new ContinueExecution<TTypeResult, TDbConnection>(_statements, 
                new ContinueExecutionResult2<TDbConnection, TReturn, TTypeResult>(_statements, exec, _databaseManagement), _databaseManagement);
        }
    }
}
