using GSqlQuery.Runner;
using GSqlQuery.Runner.Models;

namespace GSqlQuery.MySql
{
    public class MySqlConnectionOptions : ConnectionOptions<MySqlDatabaseConnection>
    {
        public MySqlConnectionOptions(string connectionString) : 
            base(new MySqlStatements(),new MySqlDatabaseManagment(connectionString))
        {}

        public MySqlConnectionOptions(string connectionString, DatabaseManagmentEvents events) : 
            base(new MySqlStatements(), new MySqlDatabaseManagment(connectionString, events))
        {}

        public MySqlConnectionOptions(IStatements statements, MySqlDatabaseManagment mySqlDatabaseManagment) :
            base(statements, mySqlDatabaseManagment)
        {}
    }
}
