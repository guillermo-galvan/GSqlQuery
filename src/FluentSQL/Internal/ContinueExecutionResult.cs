using FluentSQL.Default;
using FluentSQL.Models;

namespace FluentSQL.Internal
{
    public abstract class ContinueExecutionResult<TDbConnection>
    {
        protected readonly ConnectionOptions<TDbConnection> _connectionOptions;
        protected Queue<ContinueExecutionResult<TDbConnection>> _fifo = new();

        protected abstract object? Execute(TDbConnection connection,object? param);

        public ContinueExecutionResult(ConnectionOptions<TDbConnection> connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
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

    internal class ContinueExecutionQueryBuilderResult<T, TReturn,TDbConnection, TResult, TNewResult> : ContinueExecutionResult<TDbConnection>
        where T : class, new() where TReturn : IQuery<T, TDbConnection, TNewResult>
    {
        private readonly Func<ConnectionOptions<TDbConnection>, TResult, IQueryBuilder<T, TReturn, TDbConnection, TNewResult>>? _func;

        public ContinueExecutionQueryBuilderResult(ConnectionOptions<TDbConnection> connectionOptions,
            Func<ConnectionOptions<TDbConnection>, TResult, IQueryBuilder<T, TReturn, TDbConnection, TNewResult>> func) :
            base(connectionOptions)
        {
            _func = func;
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            if (_func != null)
            {
                return _func.Invoke(_connectionOptions, (TResult)param!).Build().Exec(connection);
            }

            return default(TResult);
        }
    }

    internal class ContinueExecutionQueryBuilderResult<T, TReturn, TDbConnection, TNewResult> : ContinueExecutionResult<TDbConnection>
        where T : class, new() where TReturn : IQuery<T, TDbConnection, TNewResult>
    {
        private readonly Func<ConnectionOptions<TDbConnection>, IQueryBuilder<T, TReturn, TDbConnection, TNewResult>>? _func;

        public ContinueExecutionQueryBuilderResult(ConnectionOptions<TDbConnection> connectionOptions,
            Func<ConnectionOptions<TDbConnection>, IQueryBuilder<T, TReturn, TDbConnection, TNewResult>> func) :
            base(connectionOptions)
        {
            _func = func;
            Add(this);
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            if (_func != null)
            {
                return _func.Invoke(_connectionOptions).Build().Exec(connection);
            }

            return default(TNewResult);
        }
    }

    internal class ContinueExecutionIAndOrResult<T, TReturn, TDbConnection, TNewResult> : ContinueExecutionResult<TDbConnection>
        where T : class, new() where TReturn : IQuery<T, TDbConnection, TNewResult>
    {
        private readonly Func<ConnectionOptions<TDbConnection>, IAndOr<T, TReturn, TDbConnection, TNewResult>>? _func;

        public ContinueExecutionIAndOrResult(ConnectionOptions<TDbConnection> connectionOptions,
            Func<ConnectionOptions<TDbConnection>, IAndOr<T, TReturn, TDbConnection, TNewResult>> func) :
            base(connectionOptions)
        {
            _func = func;
            Add(this);
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            if (_func != null)
            {
                return _func.Invoke(_connectionOptions).Build().Exec(connection);
            }

            return default(TNewResult);
        }
    }

    internal class ContinueExecutionIAndOrResult<T, TReturn, TDbConnection, TResult, TNewResult> : ContinueExecutionResult<TDbConnection>
        where T : class, new() where TReturn : IQuery<T, TDbConnection, TNewResult>
    {
        private readonly Func<ConnectionOptions<TDbConnection>, TResult, IAndOr<T, TReturn, TDbConnection, TNewResult>>? _func;

        public ContinueExecutionIAndOrResult(ConnectionOptions<TDbConnection> connectionOptions,
            Func<ConnectionOptions<TDbConnection>, TResult, IAndOr<T, TReturn, TDbConnection, TNewResult>> func) :
            base(connectionOptions)
        {
            _func = func;
        }

        protected override object? Execute(TDbConnection connection, object? param)
        {
            if (_func != null)
            {
                return _func.Invoke(_connectionOptions, (TResult)param!).Build().Exec(connection);
            }

            return default(TResult);
        }
    }
}
