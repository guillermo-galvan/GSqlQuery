using GSqlQuery.Sqlite;
using GSqlQuery.Sqlite.Test.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Sqlite.Test
{
    public class SqliteDatabaseManagmentEventsTest
    {
        private readonly SqliteConnectionOptions _connectionOptions;

        public SqliteDatabaseManagmentEventsTest()
        {
            _connectionOptions = new SqliteConnectionOptions(Helper.ConnectionString);
        }
    }
}