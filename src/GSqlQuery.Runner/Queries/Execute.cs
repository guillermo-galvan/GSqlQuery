using GSqlQuery.Runner;
using System;

namespace GSqlQuery
{
    public class Execute
    {
        public static BatchExecute<TDbConnection> BatchExecuteFactory<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            if (connectionOptions == null)
            {
                throw new ArgumentNullException(nameof(connectionOptions), ErrorMessages.ParameterNotNull);
            }

            return new BatchExecute<TDbConnection>(connectionOptions);
        }
    }
}