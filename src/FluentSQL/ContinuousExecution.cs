using FluentSQL.Extensions;
using FluentSQL.Internal;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    public sealed class ContinuousExecution
    {
        public readonly ConnectionOptions _connectionOptions;

        public ContinuousExecution(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        public ContinueExecution<TResult> New<TResult>(Func<ConnectionOptions, IExecute<TResult>> query)
        {
            query.NullValidate(ErrorMessages.ParameterNotNull, nameof(query));
            return new ContinueExecution<TResult>(_connectionOptions, new ContinueExecutionResult<TResult, TResult>(_connectionOptions, query));
        }
    }
}
