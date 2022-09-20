using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL
{
    public class Execute
    {
        public static ContinuousExecution<TDbConnection> ContinuousExecutionFactory<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNull, nameof(connectionOptions));
            return new ContinuousExecution<TDbConnection>(connectionOptions);
        }

        public static BatchExecute<TDbConnection> BatchExecuteFactory<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNull, nameof(connectionOptions));
            return new BatchExecute<TDbConnection>(connectionOptions);
        }
    }
}
