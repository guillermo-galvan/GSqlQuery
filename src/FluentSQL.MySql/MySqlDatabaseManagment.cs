using FluentSQL.Models;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override IEnumerable<T> ExecuteReader<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            IEnumerable<T> result = ExecuteReader(connection, query, propertyOptions, parameters);
            connection.Close();
            return result;
        }

        public override IEnumerable<T> ExecuteReader<T>(MySqlConnection connection, IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
        {
            ITransformTo<T> transformToEntity = GetTransformTo<T>();
            List<T> result = new();
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

                result.Add(transformToEntity.Generate());
            }

            return result;
        }

        public override object ExecuteScalar(IQuery query, IEnumerable<IDataParameter> parameters, Type resultType)
        {
            using MySqlConnection connection = new(_connectionString);
            connection.Open();
            object result = ExecuteScalar(connection, query, parameters, resultType);
            connection.Close();
            return result;
        }

        public override object ExecuteScalar(MySqlConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, Type resultType)
        {
            using var command = connection.CreateCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            object result = command.ExecuteScalar();
            result = SwitchTypeValue(resultType, result);
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
