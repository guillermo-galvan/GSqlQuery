using FluentSQL.DataBase;
using FluentSQL.Models;

namespace FluentSQL.MySql
{
    public class MySqlConnectionOptions : ConnectionOptions<MySqlDatabaseConnection>
    {
        public MySqlConnectionOptions(string connectionString) : base(new MySqlStatements(),new MySqlDatabaseManagment(connectionString))
        {
        }
    }
}
