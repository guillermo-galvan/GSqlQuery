using FluentSQL.Models;
using Microsoft.Data.SqlClient;

namespace FluentSQL.SQLServer
{
    public class SqlServerConnectionOptions : ConnectionOptions<SqlConnection>
    {
        public SqlServerConnectionOptions(SqlServerStatements statements,
            SqlServerDatabaseManagment databaseManagment) : base(statements, databaseManagment)
        {
        }
    }
}
