using GSqlQuery.Runner;

namespace GSqlQuery.Sqlite
{
    public class SqliteConnectionOptions : ConnectionOptions<SqliteDatabaseConnection>
    {
        public SqliteConnectionOptions(string connectionString) : 
            base(new SqliteStatements(), new SqliteDatabaseManagment(connectionString))
        { }

        public SqliteConnectionOptions(string connectionString, DatabaseManagmentEvents events) :
            base(new SqliteStatements(), new SqliteDatabaseManagment(connectionString, events))
        { }

        public SqliteConnectionOptions(IStatements statements, SqliteDatabaseManagment sqlServerDatabaseManagment) :
            base(statements, sqlServerDatabaseManagment)
        {

        }


    }
}
