using FluentSQL.Default;

namespace FluentSQL.Internal
{
    public abstract class ContinueExecutionResult<TDbConnection>
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

    internal class ContinueExecutionResult<TDbConnection, TResult> : ContinueExecutionResult<TDbConnection>
    {
        private readonly Func<IStatements, ISetDatabaseManagement<TResult>>? _func;        

        public ContinueExecutionResult(IStatements statements, Func<IStatements, ISetDatabaseManagement<TResult>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements, databaseManagement)
        {
            _func = query;
            _fifo.Enqueue(this);
        }

        protected override object? Execute(TDbConnection connection,object? param)
        {
            if (_func != null)
            {
                return _func.Invoke(_statements).SetDatabaseManagement(_databaseManagement).Exec(connection);
            }

            return default(TResult);
        }
    }

    internal class ContinueExecutionResult<TDbConnection, TResult, TNewResult> : ContinueExecutionResult<TDbConnection>
    {
        private readonly Func<IStatements, TResult, ISetDatabaseManagement<TNewResult>>? _func;

        public ContinueExecutionResult(IStatements statements, Func<IStatements, TResult, ISetDatabaseManagement<TNewResult>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements, databaseManagement)
        {
            _func = query;
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            if(_func != null)
            {
                return _func.Invoke(_statements, (TResult)param).SetDatabaseManagement(_databaseManagement).Exec(connection);
            }

            return default(TResult);
        }
    }

    internal class ContinueExecutionResult2 <TDbConnection, TResult, TReturn, TTypeResult> : ContinueExecutionResult<TDbConnection>
        where TReturn : ISetDatabaseManagement<TTypeResult> where TTypeResult : struct
    {
        private readonly Func<IStatements, TResult, IBuilder<TReturn>>? _func;

        public ContinueExecutionResult2(IStatements statements, Func<IStatements, TResult, IBuilder<TReturn>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements, databaseManagement)
        {
            _func = query;
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            return _func?.Invoke(_statements, (TResult)param).Build().SetDatabaseManagement(_databaseManagement).Exec(connection);
        }
    }

    internal class ContinueExecutionResult2<TDbConnection, TReturn, TTypeResult> : ContinueExecutionResult<TDbConnection>
        where TReturn : ISetDatabaseManagement<TTypeResult> where TTypeResult : struct
    {
        private readonly Func<IStatements, IBuilder<TReturn>>? _func;

        public ContinueExecutionResult2(IStatements statements, Func<IStatements, IBuilder<TReturn>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements, databaseManagement)
        {
            _func = query;
            _fifo.Enqueue(this);
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            return _func?.Invoke(_statements).Build().SetDatabaseManagement(_databaseManagement).Exec(connection);
        }
    }

    internal class ContinueExecutionResult3<TDbConnection, TResult, T> : ContinueExecutionResult<TDbConnection>
        where T : class, new()
    {
        private readonly Func<IStatements, TResult, IBuilder<SelectQuery<T>>>? _func;

        public ContinueExecutionResult3(IStatements statements, Func<IStatements, TResult, IBuilder<SelectQuery<T>>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements, databaseManagement)
        {
            _func = query;
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            return _func?.Invoke(_statements, (TResult)param).Build().SetDatabaseManagement(_databaseManagement).Exec(connection);
        }
    }

    internal class ContinueExecutionResult3<TDbConnection, T> : ContinueExecutionResult<TDbConnection>
        where T : class, new()
    {
        private readonly Func<IStatements,  IBuilder<SelectQuery<T>>>? _func;

        public ContinueExecutionResult3(IStatements statements, Func<IStatements, IBuilder<SelectQuery<T>>> query,
            IDatabaseManagement<TDbConnection> databaseManagement) :
            base(statements, databaseManagement)
        {
            _func = query;
            _fifo.Enqueue(this);
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            return _func?.Invoke(_statements).Build().SetDatabaseManagement(_databaseManagement).Exec(connection);
        }
    }
}
