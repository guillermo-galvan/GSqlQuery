using FluentSQL.Default;
using FluentSQL.Internal;
using FluentSQL.Models;
using System.Security.Principal;

namespace FluentSQL
{
    public sealed class ContinuousExecution<TDbConnection>
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;

        public ContinuousExecution(ConnectionOptions<TDbConnection> connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        public ContinueExecution<TResult, TDbConnection> New<T, TReturn, TResult>
            (Func<ConnectionOptions<TDbConnection>, IQueryBuilder<T, TReturn, TDbConnection, TResult>> query)
            where T : class, new() where TReturn : IQuery<T, TDbConnection, TResult>
        {
            return new ContinueExecution<TResult, TDbConnection>(_connectionOptions,
                new ContinueExecutionQueryBuilderResult<T, TReturn, TDbConnection, TResult>(_connectionOptions, query));
        }

        public ContinueExecution<TResult, TDbConnection> New<T, TReturn, TResult>
            (Func<ConnectionOptions<TDbConnection>, IAndOr<T, TReturn, TDbConnection, TResult>> query)
            where T : class, new() where TReturn : IQuery<T, TDbConnection, TResult>
        {
            return new ContinueExecution<TResult, TDbConnection>(_connectionOptions,
                new ContinueExecutionIAndOrResult<T, TReturn, TDbConnection, TResult>(_connectionOptions, query));
        }
    }
}
