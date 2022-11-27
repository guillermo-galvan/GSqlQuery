using FluentSQL.DataBase;
using FluentSQL.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FluentSQL.SQLServer
{
    public class SqlServerDatabaseManagment : DatabaseManagment, IDatabaseManagement<SqlServerDatabaseConnection>
    {
        public SqlServerDatabaseManagment(string connectionString) :
            base(connectionString, new SqlServerDatabaseManagmentEvents())
        { }

        public SqlServerDatabaseManagment(string connectionString, DatabaseManagmentEvents events) : base(connectionString, events)
        { }

        public SqlServerDatabaseManagment(string connectionString, DatabaseManagmentEvents events, ILogger? logger) : base(connectionString, events, logger)
        { }

        public int ExecuteNonQuery(SqlServerDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            return base.ExecuteNonQuery(connection, query, parameters);
        }

        public Task<int> ExecuteNonQueryAsync(SqlServerDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            return base.ExecuteNonQueryAsync(connection, query, parameters, cancellationToken);
        }

        public IEnumerable<T> ExecuteReader<T>(SqlServerDatabaseConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters) where T : class, new()
        {
            return base.ExecuteReader<T>(connection, query, propertyOptions, parameters);
        }

        public Task<IEnumerable<T>> ExecuteReaderAsync<T>(SqlServerDatabaseConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default) where T : class, new()
        {
            return base.ExecuteReaderAsync<T>(connection, query, propertyOptions, parameters, cancellationToken);
        }

        public T ExecuteScalar<T>(SqlServerDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            return base.ExecuteScalar<T>(connection, query, parameters);
        }

        public Task<T> ExecuteScalarAsync<T>(SqlServerDatabaseConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            return base.ExecuteScalarAsync<T>(connection, query, parameters, cancellationToken);
        }

        public override SqlServerDatabaseConnection GetConnection()
        {
            SqlServerDatabaseConnection databaseConnection = new(_connectionString);

            if (databaseConnection.State != ConnectionState.Open)
            {
                databaseConnection.Open();
            }

            return databaseConnection;
        }

        public async override Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            SqlServerDatabaseConnection databaseConnection = new(_connectionString);

            if (databaseConnection.State != ConnectionState.Open)
            {
                await databaseConnection.OpenAsync(cancellationToken);
            }

            return databaseConnection;
        }

        async Task<SqlServerDatabaseConnection> IDatabaseManagement<SqlServerDatabaseConnection>.GetConnectionAsync(CancellationToken cancellationToken)
        {
            return (SqlServerDatabaseConnection)await GetConnectionAsync(cancellationToken);
        }
    }
}
