using FluentSQL.Models;

namespace FluentSQL.SQLServer
{
    public class SqlServerConnectionOptions : ConnectionOptions<SqlServerDatabaseConnection>
    {
        public SqlServerConnectionOptions(SqlServerStatements statements,
            SqlServerDatabaseManagment databaseManagment) : base(statements, databaseManagment)
        {
        }
    }
}
