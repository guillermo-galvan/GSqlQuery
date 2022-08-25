using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Internal
{
    internal abstract class ContinueExecutionResult
    {
        protected readonly ConnectionOptions _connectionOptions;
        protected Queue<ContinueExecutionResult> _fifo = new();

        protected abstract object? Execute(DbConnection connection,object? param);

        public ContinueExecutionResult(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions)); ;
        }

        public void Add(ContinueExecutionResult continueExecutionResult)
        {
            _fifo.Enqueue(continueExecutionResult);
        }

        public object? Start(DbConnection connection)
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

    internal class ContinueExecutionResult<TResult, TNewResult> : ContinueExecutionResult
    {
        private readonly Func<ConnectionOptions, IExecute<TResult>>? _first;
        private readonly Func<ConnectionOptions, TResult, IExecute<TNewResult>>? _second;

        public ContinueExecutionResult(ConnectionOptions connectionOptions, Func<ConnectionOptions, IExecute<TResult>> query) : base(connectionOptions)
        {
            _first = query;
            _fifo.Enqueue(this);
        }

        public ContinueExecutionResult(ConnectionOptions connectionOptions, Func<ConnectionOptions, TResult, IExecute<TNewResult>> query) : base(connectionOptions)
        {
            _second = query;
        }

        protected override object? Execute(DbConnection connection,object? param)
        {
            if (_second != null)
            {
                return _second.Invoke(_connectionOptions, (TResult)param).Exec(connection);
            }

            if (_first != null)
            {
                return _first.Invoke(_connectionOptions).Exec(connection);
            }

            return null;
        }
    }
}
