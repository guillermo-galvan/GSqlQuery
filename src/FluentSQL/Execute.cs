using FluentSQL.Models;

namespace FluentSQL
{
    public class Execute
    {
        public static ContinuousExecution ContinuousExecutionFactory(ConnectionOptions connectionOptions)
        {
            return new ContinuousExecution(connectionOptions);
        }

        public static BatchExecute BatchExecuteFactory(ConnectionOptions connectionOptions)
        {
            return new BatchExecute(connectionOptions);
        }
    }
}
