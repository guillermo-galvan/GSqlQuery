using FluentSQL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;

namespace FluentSQL.SQLServer
{
    public class SqlServerDatabaseManagment : DatabaseManagment<SqlConnection>, IDatabaseManagement<SqlConnection>
    {
        public SqlServerDatabaseManagment(string connectionString) : base(connectionString, new SqlServerDatabaseManagmentEvents())
        {

        }

        public SqlServerDatabaseManagment(string connectionString, ILogger<SqlServerDatabaseManagment>? logger) :
         base(connectionString, new SqlServerDatabaseManagmentEvents(), logger)
        {

        }

        public SqlServerDatabaseManagment(string connectionString, DatabaseManagmentEvents events, ILogger<SqlServerDatabaseManagment>? logger) :
         base(connectionString, events, logger)
        {

        }

        public override int ExecuteNonQuery(IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            int result = ExecuteNonQuery(connection, query, parameters);
            connection.Close();
            return result;
        }

        public override int ExecuteNonQuery(SqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            if (Events.IsTraceActive)
            {
                _logger?.LogDebug("ExecuteNonQuery Query: {@Text} Parameters: {@parameters} ", query.Text, parameters);
            }

            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return command.ExecuteNonQuery();
        }

        public override async Task<int> ExecuteNonQueryAsync(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync(cancellationToken);
            int result = await ExecuteNonQueryAsync(connection, query, parameters, cancellationToken);
            await connection.CloseAsync();
            return result;
        }

        public override Task<int> ExecuteNonQueryAsync(SqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (Events.IsTraceActive)
            {
                _logger?.LogDebug("ExecuteNonQueryAsync Query: {@Text} Parameters: {@parameters} ", query.Text, parameters);
            }
            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return command.ExecuteNonQueryAsync(cancellationToken);
        }

        public override IEnumerable<T> ExecuteReader<T>(IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            IEnumerable<T> result = ExecuteReader<T>(connection, query, propertyOptions, parameters);
            connection.Close();
            return result;
        }

        public override IEnumerable<T> ExecuteReader<T>(SqlConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
        {
            if (Events.IsTraceActive)
            {
                _logger?.LogDebug("ExecuteReader Type: {@FullName} Query: {@Text} Parameters: {@parameters} ", typeof(T).FullName, query.Text, parameters);
            }
            ITransformTo<T> transformToEntity = GetTransformTo<T>();
            Queue<T> result = new();
            object? valor = null;

            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            var columns = (from pro in propertyOptions
                           join ca in query.Columns on pro.ColumnAttribute.Name equals ca.Name into leftJoin
                           from left in leftJoin.DefaultIfEmpty()
                           select new { Property = pro, Column = left, IsColumnInQuery = left is not null }).ToList();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                columns.ForEach(x => {
                    if (x.IsColumnInQuery)
                    {
                        valor = reader.GetValue(x.Column.Name);
                        valor = SwitchTypeValue(x.Property.PropertyInfo.PropertyType, valor);
                    }
                    else
                    {
                        valor = x.Property.PropertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(x.Property.PropertyInfo.PropertyType) : null;
                    }
                    transformToEntity.SetValue(x.Property.PositionConstructor, x.Property.PropertyInfo.Name, valor);
                });

                result.Enqueue(transformToEntity.Generate());
            }

            return result;
        }

        public override async Task<IEnumerable<T>> ExecuteReaderAsync<T>(IQuery query, IEnumerable<PropertyOptions> propertyOptions,
            IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync(cancellationToken);
            IEnumerable<T> result = await ExecuteReaderAsync<T>(connection, query, propertyOptions, parameters, cancellationToken);
            await connection.CloseAsync();
            return result;
        }

        public override async Task<IEnumerable<T>> ExecuteReaderAsync<T>(SqlConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions,
            IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (Events.IsTraceActive)
            {
                _logger?.LogDebug("ExecuteReaderAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters} ", typeof(T).FullName, query.Text, parameters);
            }
            ITransformTo<T> transformToEntity = GetTransformTo<T>();
            Queue<T> result = new();
            object? valor = null;

            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            var columns = (from pro in propertyOptions
                           join ca in query.Columns on pro.ColumnAttribute.Name equals ca.Name into leftJoin
                           from left in leftJoin.DefaultIfEmpty()
                           select new { Property = pro, Column = left, IsColumnInQuery = left is not null }).ToList();

            using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync())
            {
                columns.ForEach(x => {
                    if (x.IsColumnInQuery)
                    {
                        valor = reader.GetValue(x.Column.Name);
                        valor = SwitchTypeValue(x.Property.PropertyInfo.PropertyType, valor);
                    }
                    else
                    {
                        valor = x.Property.PropertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(x.Property.PropertyInfo.PropertyType) : null;
                    }
                    transformToEntity.SetValue(x.Property.PositionConstructor, x.Property.PropertyInfo.Name, valor);
                });

                result.Enqueue(transformToEntity.Generate());
            }

            return result;
        }

        public override T ExecuteScalar<T>(IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            T result = ExecuteScalar<T>(connection, query, parameters);
            connection.Close();
            return result;
        }

        public override T ExecuteScalar<T>(SqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            if (Events.IsTraceActive)
            {
                _logger?.LogDebug("ExecuteScalar Type: {@FullName} Query: {@Text} Parameters: {@parameters} ", typeof(T).FullName, query.Text, parameters);
            }
            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            object resultCommand = command.ExecuteScalar();
            T result = (T)SwitchTypeValue(typeof(T), resultCommand)!;
            return result;
        }

        public override async Task<T> ExecuteScalarAsync<T>(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync(cancellationToken);
            T? result = await ExecuteScalarAsync<T>(connection, query, parameters, cancellationToken);
            await connection.CloseAsync();
            return result;
        }

        public override async Task<T> ExecuteScalarAsync<T>(SqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (Events.IsTraceActive)
            {
                _logger?.LogDebug("ExecuteScalarAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters} ", typeof(T).FullName, query.Text, parameters);
            }
            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            object? resultCommand = await command.ExecuteScalarAsync(cancellationToken);
            T result = (T)SwitchTypeValue(typeof(T), resultCommand)!;
            return result;
        }

        public override SqlConnection GetConnection()
        {
            SqlConnection result = new(_connectionString);
            if (result.State != ConnectionState.Open)
            {
                result.Open();
            }
            return result;
        }

        public override async Task<SqlConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            SqlConnection result = new(_connectionString);

            if (result.State != ConnectionState.Open)
            {
                await result.OpenAsync(cancellationToken);
            }
            return result;
        }
    }
}
