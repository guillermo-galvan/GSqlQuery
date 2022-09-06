using FluentSQL.Models;

namespace FluentSQL
{
    public class Execute
    {
        public static ContinuousExecution<TDbConnection> ContinuousExecutionFactory<TDbConnection>(IStatements statements, 
            IDatabaseManagement<TDbConnection> databaseManagement)
        {
            return new ContinuousExecution<TDbConnection>(statements, databaseManagement);
        }

        public static BatchExecute<TDbConnection> BatchExecuteFactory<TDbConnection>(IStatements statements, IDatabaseManagement<TDbConnection> databaseManagement)
        {
            return new BatchExecute<TDbConnection>(statements, databaseManagement);
        }
    }
}
