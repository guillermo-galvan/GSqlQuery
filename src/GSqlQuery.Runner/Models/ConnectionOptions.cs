namespace GSqlQuery.Runner.Models
{
    public class ConnectionOptions<TDbConnection>
    {
        public IStatements Statements { get; }

        public IDatabaseManagement<TDbConnection> DatabaseManagment { get; }

        public ConnectionOptions(IStatements statements, IDatabaseManagement<TDbConnection> databaseManagment)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
            DatabaseManagment = databaseManagment ?? throw new ArgumentNullException(nameof(databaseManagment));
        }
    }
}
