using GSqlQuery.Runner;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GSqlQuery.MySql
{
    public sealed class MySqlDatabaseManagment : DatabaseManagment, IDatabaseManagement<MySqlDatabaseConnection>
    {
        public MySqlDatabaseManagment(string connectionString) :
            base(connectionString, new MySqlDatabaseManagmentEvents())
        {}

        public MySqlDatabaseManagment(string connectionString, DatabaseManagmentEvents events) : base(connectionString, events)
        {}

        public MySqlDatabaseManagment(string connectionString, DatabaseManagmentEvents events, ILogger? logger) : base(connectionString, events, logger)
        {}

        public int ExecuteNonQuery(MySqlDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            return base.ExecuteNonQuery(connection, query, parameters);
        }

        public Task<int> ExecuteNonQueryAsync(MySqlDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            return base.ExecuteNonQueryAsync(connection, query, parameters, cancellationToken);
        }

        public IEnumerable<T> ExecuteReader<T>(MySqlDatabaseConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters) where T : class, new()
        {
            return base.ExecuteReader<T>(connection, query, propertyOptions, parameters);
        }

        public Task<IEnumerable<T>> ExecuteReaderAsync<T>(MySqlDatabaseConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default) where T : class, new()
        {
            return base.ExecuteReaderAsync<T>(connection,query, propertyOptions, parameters, cancellationToken);
        }

        public T ExecuteScalar<T>(MySqlDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            return base.ExecuteScalar<T>(connection, query, parameters);
        }

        public Task<T> ExecuteScalarAsync<T>(MySqlDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            return base.ExecuteScalarAsync<T>(connection, query, parameters, cancellationToken);
        }

        public override MySqlDatabaseConnection GetConnection()
        {
            MySqlDatabaseConnection mySqlDatabase = new(_connectionString);

            if (mySqlDatabase.State != ConnectionState.Open)
            {
                mySqlDatabase.Open();
            }

            return mySqlDatabase; 
        }

        public async override Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            MySqlDatabaseConnection databaseConnection = new(_connectionString);

            if (databaseConnection.State != ConnectionState.Open)
            {
                await databaseConnection.OpenAsync(cancellationToken);
            }

            return databaseConnection;
        }

        async Task<MySqlDatabaseConnection> IDatabaseManagement<MySqlDatabaseConnection>.GetConnectionAsync(CancellationToken cancellationToken)
        {
            return (MySqlDatabaseConnection)await GetConnectionAsync(cancellationToken);
        }
    }
}
