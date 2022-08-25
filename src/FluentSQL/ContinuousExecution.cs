using FluentSQL.Extensions;
using FluentSQL.Internal;
using FluentSQL.Models;

namespace FluentSQL
{
    public sealed class ContinuousExecution
    {
        public readonly ConnectionOptions _connectionOptions;

        public ContinuousExecution(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            connectionOptions.DatabaseManagment.ValidateDatabaseManagment();
        }

        public ContinueExecution<TResult> New<TResult>(Func<ConnectionOptions, IExecute<TResult>> query)
        {
            return new ContinueExecution<TResult>(_connectionOptions, new ContinueExecutionResult<TResult, TResult>(_connectionOptions, query));
        }
    }
}
