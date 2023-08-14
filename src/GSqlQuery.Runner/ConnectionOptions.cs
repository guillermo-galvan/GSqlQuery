using System;

namespace GSqlQuery
{
    public class ConnectionOptions<TDbConnection>
    {
        public IStatements Statements { get; }

        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        public ConnectionOptions(IStatements statements, IDatabaseManagement<TDbConnection> databaseManagement)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
            DatabaseManagement = databaseManagement ?? throw new ArgumentNullException(nameof(databaseManagement));
        }
    }
}