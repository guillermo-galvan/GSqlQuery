using FluentSQL.Models;

namespace FluentSQL.SQLServer
{
    public class SqlServerConnectionOptions : ConnectionOptions<SqlServerDatabaseConnection>
    {
        public SqlServerConnectionOptions(string connectionString) : base(new SqlServerStatements(), 
            new SqlServerDatabaseManagment(connectionString))
        {}

        public SqlServerConnectionOptions(string connectionString, DatabaseManagmentEvents events) : 
            base(new SqlServerStatements(), new SqlServerDatabaseManagment(connectionString, events))
        {}

        public SqlServerConnectionOptions(SqlServerStatements sqlServerStatements, SqlServerDatabaseManagment sqlServerDatabaseManagment) :
            base(sqlServerStatements, sqlServerDatabaseManagment)
        {

        }
    }
}
