using FluentSQL.Sqlite;

namespace FluentSQL.SqliteTest
{
    public class SqliteConnectionOptionsTest
    {
        [Fact]
        public void Create_SqliteConnectionOptions_With_ConnectionString()
        {
            var SqliteConnectionOptions = new SqliteConnectionOptions(Helper.ConnectionString);
            Assert.NotNull(SqliteConnectionOptions);
        }

        [Fact]
        public void Create_SqliteConnectionOptions_With_ConnectionString_and_events()
        {
            var SqliteConnectionOptions = new SqliteConnectionOptions(Helper.ConnectionString, new SqliteDatabaseManagmentEvents());
            Assert.NotNull(SqliteConnectionOptions);
        }

        [Fact]
        public void Create_SqliteConnectionOptions_With_statements_and_sqlServerDatabaseManagment()
        {
            var SqliteConnectionOptions = new SqliteConnectionOptions(new SqliteStatements(), new SqliteDatabaseManagment(Helper.ConnectionString));
            Assert.NotNull(SqliteConnectionOptions);
        }
    }
}