using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;

namespace FluentSQL.DatabaseManagement
{
    public class Execute
    {
        public static BatchExecute<TDbConnection> BatchExecuteFactory<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNull, nameof(connectionOptions));
            return new BatchExecute<TDbConnection>(connectionOptions);
        }
    }
}
