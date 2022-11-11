using FluentSQL.Models;
using MySql.Data.MySqlClient;

namespace FluentSQL.MySql
{
    public class MySqlConnectionOptions : ConnectionOptions<MySqlConnection>
    {
        public MySqlConnectionOptions(MySqlStatements statements,
            MySqlDatabaseManagment databaseManagment) : base(statements, databaseManagment)
        {
        }
    }
}
