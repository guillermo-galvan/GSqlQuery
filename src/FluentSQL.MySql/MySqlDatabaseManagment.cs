using FluentSQL.Models;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace FluentSQL.MySql
{
    public class MySqlDatabaseManagment : DatabaseManagment<MySqlConnection>, IDatabaseManagement<MySqlConnection>
    {
        public MySqlDatabaseManagment(string connectionString) : base(connectionString, new MySqlDatabaseManagmentEvents())
        {

        }

        public MySqlDatabaseManagment(string connectionString, ILogger<MySqlDatabaseManagment>? logger) :
         base(connectionString, new MySqlDatabaseManagmentEvents(), logger)
        {

        }

        public MySqlDatabaseManagment(string connectionString, DatabaseManagmentEvents events, ILogger<MySqlDatabaseManagment>? logger) :
         base(connectionString, events, logger)
        {

        }

        public override int ExecuteNonQuery(IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            int result = ExecuteNonQuery(connection, query, parameters);
            connection.Close();
            return result;
        }

        public override int ExecuteNonQuery(MySqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return command.ExecuteNonQuery();
        }

        public override async Task<int> ExecuteNonQueryAsync(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            int result = await ExecuteNonQueryAsync(connection, query, parameters, cancellationToken);
            connection.Close();
            return result;
        }

        public override Task<int> ExecuteNonQueryAsync(MySqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return command.ExecuteNonQueryAsync(cancellationToken);
        }

        public override IEnumerable<T> ExecuteReader<T>(IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            IEnumerable<T> result = ExecuteReader<T>(connection, query, propertyOptions, parameters);
            connection.Close();
            return result;
        }

        public override IEnumerable<T> ExecuteReader<T>(MySqlConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
        {
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

            using MySqlDataReader reader = command.ExecuteReader();

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
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            IEnumerable<T> result = await ExecuteReaderAsync<T>(connection, query, propertyOptions, parameters, cancellationToken);
            connection.Close();
            return result;
        }

        public override async Task<IEnumerable<T>> ExecuteReaderAsync<T>(MySqlConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, 
            IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
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
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            T result = ExecuteScalar<T>(connection, query, parameters);
            connection.Close();
            return result;
        }

        public override T ExecuteScalar<T>(MySqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
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
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            T? result = await ExecuteScalarAsync<T>(connection, query, parameters, cancellationToken);
            connection.Close();
            return result;
        }

        public override async Task<T> ExecuteScalarAsync<T>(MySqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            object? resultCommand = await command.ExecuteScalarAsync(cancellationToken);
            T result = (T)SwitchTypeValue(typeof(T), resultCommand)!;
            return result;
        }

        public override MySqlConnection GetConnection()
        {
            MySqlConnection result = new MySqlConnection(_connectionString);
            if (result.State != ConnectionState.Open)
            {
                result.Open();
            }
            return result;
        }
    }
}
