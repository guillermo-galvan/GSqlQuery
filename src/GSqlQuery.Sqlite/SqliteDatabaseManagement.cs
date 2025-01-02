using GSqlQuery.Runner;
using GSqlQuery.Runner.TypeHandles;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Sqlite
{
    public sealed class SqliteDatabaseManagement(string connectionString, SqliteDatabaseManagementEvents events) : DatabaseManagement<SqliteDatabaseConnection, SqliteDatabaseTransaction, SqliteCommand, SqliteTransaction, SqliteDataReader>(connectionString, events), IDatabaseManagement<SqliteDatabaseConnection>
    {
        public SqliteDatabaseManagement(string connectionString) : this(connectionString, new SqliteDatabaseManagementEvents())
        { }

        public override SqliteDatabaseConnection GetConnection()
        {
            SqliteDatabaseConnection databaseConnection = new SqliteDatabaseConnection(_connectionString);

            if (databaseConnection.State != ConnectionState.Open)
            {
                databaseConnection.Open();
            }

            return databaseConnection;
        }

        public override async Task<SqliteDatabaseConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SqliteDatabaseConnection databaseConnection = new SqliteDatabaseConnection(_connectionString);

            if (databaseConnection.State != ConnectionState.Open)
            {
                await databaseConnection.OpenAsync(cancellationToken);
            }

            return databaseConnection;
        }
    }
}