namespace FluentSQL.Internal
{
    internal abstract class ContinueExecutionResult<TDbConnection>
    {
        protected readonly IStatements _statements;
        protected Queue<ContinueExecutionResult<TDbConnection>> _fifo = new();
        protected readonly IDatabaseManagement<TDbConnection> _databaseManagement;

        protected abstract object? Execute(TDbConnection connection,object? param);

        public ContinueExecutionResult(IStatements statements, IDatabaseManagement<TDbConnection> databaseManagement)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _databaseManagement = databaseManagement ?? throw new ArgumentNullException(nameof(databaseManagement));
        }

        public void Add(ContinueExecutionResult<TDbConnection> continueExecutionResult)
        {
            _fifo.Enqueue(continueExecutionResult);
        }

        public object? Start(TDbConnection connection)
        {
            object? result = null;

            foreach (var item in _fifo)
            {
                result = item.Execute(connection,result);
            }

            _fifo.Clear();

            return result;
        }
    }

    internal class ContinueExecutionResult<TDbConnection, TResult, TNewResult> : ContinueExecutionResult<TDbConnection>
    {
        private readonly Func<IStatements, IDatabaseManagement<TDbConnection>, IExecute<TResult, TDbConnection>>? _first;
        private readonly Func<IStatements, IDatabaseManagement<TDbConnection>, TResult, IExecute<TNewResult, TDbConnection>>? _second;

        public ContinueExecutionResult(IStatements statements, Func<IStatements, IDatabaseManagement<TDbConnection>, IExecute<TResult, TDbConnection>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements, databaseManagement)
        {
            _first = query;
            _fifo.Enqueue(this);
        }

        public ContinueExecutionResult(IStatements statements, Func<IStatements, IDatabaseManagement<TDbConnection>, TResult, IExecute<TNewResult, TDbConnection>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements,databaseManagement)
        {
            _second = query;
        }

        protected override object? Execute(TDbConnection connection,object? param)
        {
            if (_second != null)
            {
                return _second.Invoke(_statements, _databaseManagement,(TResult)param).Exec(connection);
            }

            if (_first != null)
            {
                return _first.Invoke(_statements, _databaseManagement).Exec(connection);
            }

            return null;
        }
    }
}
