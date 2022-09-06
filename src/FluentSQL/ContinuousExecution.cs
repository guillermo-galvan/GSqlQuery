using FluentSQL.Internal;

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
            Func<IStatements,IDatabaseManagement<TDbConnection>, IExecute<TResult, TDbConnection>> query)
        {
            return new ContinueExecution<TResult, TDbConnection>(_statements, 
                new ContinueExecutionResult<TDbConnection, TResult, TResult>(_statements, query, _databaseManagement), _databaseManagement);
        }
    }
}
