using FluentSQL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
