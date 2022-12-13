using GSqlQuery.Runner;
using GSqlQuery.Runner.Models;

namespace GSqlQuery.SQLServer
{
    public class SqlServerConnectionOptions : ConnectionOptions<SqlServerDatabaseConnection>
    {
        public SqlServerConnectionOptions(string connectionString) : base(new SqlServerStatements(), 
            new SqlServerDatabaseManagment(connectionString))
        {}

        public SqlServerConnectionOptions(string connectionString, DatabaseManagmentEvents events) : 
            base(new SqlServerStatements(), new SqlServerDatabaseManagment(connectionString, events))
        {}

        public SqlServerConnectionOptions(IStatements statements, SqlServerDatabaseManagment sqlServerDatabaseManagment) :
            base(statements, sqlServerDatabaseManagment)
        {

        }
    }
}
