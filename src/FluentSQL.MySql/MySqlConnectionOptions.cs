using FluentSQL.Models;

namespace FluentSQL.MySql
{
    public class MySqlConnectionOptions : ConnectionOptions<MySqlDatabaseConnection>
    {
        public MySqlConnectionOptions(string connectionString) : 
            base(new MySqlStatements(),new MySqlDatabaseManagment(connectionString))
        {}

        public MySqlConnectionOptions(string connectionString, DatabaseManagmentEvents events) : 
            base(new MySqlStatements(), new MySqlDatabaseManagment(connectionString, events))
        {}

        public MySqlConnectionOptions(MySqlStatements mySqlStatements, MySqlDatabaseManagment mySqlDatabaseManagment) :
            base(mySqlStatements, mySqlDatabaseManagment)
        {}
    }
}
