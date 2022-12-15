using GSqlQuery.Runner.Default;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;

namespace GSqlQuery.Runner
{
    public abstract class DatabaseManagment : IDatabaseManagement<IConnection>
    {
        private class ColumnsPropertyOptions
        {
            public PropertyOptions Property { get; set; }

            public bool IsColumnInQuery { get; set; }

            public ColumnAttribute Column { get; set; }

            public Type Type { get; set; }

            public object? ValueDefault { get; set; }

            public ColumnsPropertyOptions(PropertyOptions propertyOptions, bool isColumnInQuery, ColumnAttribute columnAttribute, Type type)
            {
                Property = propertyOptions;
                IsColumnInQuery = isColumnInQuery;
                Column = columnAttribute;
                Type = type;
            }
        }

        protected readonly string _connectionString;
        protected ILogger? _logger;

        public DatabaseManagmentEvents Events { get; set; }

        public string ConnectionString => _connectionString;

        public DatabaseManagment(string connectionString, DatabaseManagmentEvents events)
                : this(connectionString, events, null)
        {

        }

        public DatabaseManagment(string connectionString, DatabaseManagmentEvents events, ILogger? logger)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            Events = events ?? throw new ArgumentNullException(nameof(events));
            _logger = logger;
        }

        protected virtual object? SwitchTypeValue(Type type, object? value)
        {
            try
            {
                if (value == DBNull.Value || value == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ChangeType(value, type);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal DbCommand CreateCommand(IConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            var command = connection.GetDbCommand();
            command.CommandText = query.Text;

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return command;
        }

        private ITransformTo<T> GetTransformTo<T>() where T : class, new()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));

            if (!classOptions.IsConstructorByParam)
            {
                _logger?.LogWarning("{0} constructor with properties {1} not found", classOptions.Type.Name,
                    string.Join(", ", classOptions.PropertyOptions.Select(x => $"{x.PropertyInfo.Name}")));
                return new TransformToByField<T>(classOptions.PropertyOptions.Count());
            }
            else
            {
                return new TransformToByConstructor<T>(classOptions.PropertyOptions.Count());
            }
        }

        private ColumnsPropertyOptions[] GetColumnsPropertyOptions(IEnumerable<PropertyOptions> propertyOptions, IQuery query)
        {
            return (from pro in propertyOptions
                    join ca in query.Columns on pro.ColumnAttribute.Name equals ca.Name into leftJoin
                    from left in leftJoin.DefaultIfEmpty()
                    select
                        new ColumnsPropertyOptions(pro, left is not null, left, Nullable.GetUnderlyingType(pro.PropertyInfo.PropertyType) ?? pro.PropertyInfo.PropertyType)
                        {
                            ValueDefault = pro.PropertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(pro.PropertyInfo.PropertyType) : null
                        }
                   ).ToArray();
        }

        private T CreateObject<T>(ITransformTo<T> transformToEntity, ColumnsPropertyOptions[] columns, DbDataReader reader)
        {
            object? valor;

            foreach (var item in columns)
            {
                valor = item.IsColumnInQuery ? SwitchTypeValue(item.Type, reader.GetValue(item.Column.Name)) : item.ValueDefault;
                transformToEntity.SetValue(item.Property.PositionConstructor, item.Property.PropertyInfo.Name, valor);
            }

            return transformToEntity.Generate();
        }

        public int ExecuteNonQuery(IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using IConnection connection = GetConnection();
            try
            {
                return ExecuteNonQuery(connection, query, parameters); ;
            }
            finally
            {
                connection.Close();
            }
        }

        public int ExecuteNonQuery(IConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            Events?.OnWriteTrace(Events.IsTraceActive, _logger, "ExecuteNonQuery Query: {@Text} Parameters: {@parameters}",
             new object[] { query.Text, parameters });
            using var command = CreateCommand(connection, query, parameters);
            return command.ExecuteNonQuery();
        }

        public async Task<int> ExecuteNonQueryAsync(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using IConnection connection = await GetConnectionAsync(cancellationToken);
            try
            {
                return await ExecuteNonQueryAsync(connection, query, parameters, cancellationToken);
            }
            finally
            {
                await connection.CloseAsync(cancellationToken);
            }
        }

        public Task<int> ExecuteNonQueryAsync(IConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Events?.OnWriteTrace(Events.IsTraceActive, _logger, "ExecuteNonQueryAsync Query: {@Text} Parameters: {@parameters}",
             new object[] { query.Text, parameters });
            using var command = CreateCommand(connection, query, parameters);
            return command.ExecuteNonQueryAsync(cancellationToken);
        }

        public IEnumerable<T> ExecuteReader<T>(IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters) where T : class, new()
        {
            using IConnection connection = GetConnection();
            try
            {
                return ExecuteReader<T>(connection, query, propertyOptions, parameters);
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<T> ExecuteReader<T>(IConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters) where T : class, new()
        {
            Events?.OnWriteTrace(Events.IsTraceActive, _logger, "ExecuteReader Type: {@FullName} Query: {@Text} Parameters: {@parameters}",
              new object[] { typeof(T).FullName!, query.Text, parameters });
            ITransformTo<T> transformToEntity = GetTransformTo<T>();
            Queue<T> result = new();
            var columns = GetColumnsPropertyOptions(propertyOptions, query);

            using var command = CreateCommand(connection, query, parameters);
            using DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Enqueue(CreateObject(transformToEntity, columns, reader));
            }

            return result;
        }

        public async Task<IEnumerable<T>> ExecuteReaderAsync<T>(IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default) where T : class, new()
        {
            using IConnection connection = await GetConnectionAsync(cancellationToken);
            try
            {
                return await ExecuteReaderAsync<T>(connection, query, propertyOptions, parameters, cancellationToken);
            }
            finally
            {
                await connection.CloseAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<T>> ExecuteReaderAsync<T>(IConnection connection, IQuery query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default) where T : class, new()
        {
            cancellationToken.ThrowIfCancellationRequested();
            Events?.OnWriteTrace(Events.IsTraceActive, _logger, "ExecuteReaderAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters}",
               new object[] { typeof(T).FullName!, query.Text, parameters });
            ITransformTo<T> transformToEntity = GetTransformTo<T>();
            Queue<T> result = new();
            var columns = GetColumnsPropertyOptions(propertyOptions, query);

            using var command = CreateCommand(connection, query, parameters);
            using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                result.Enqueue(CreateObject(transformToEntity, columns, reader));
            }

            return result;
        }

        public T ExecuteScalar<T>(IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using IConnection connection = GetConnection();
            try
            {
                return ExecuteScalar<T>(connection, query, parameters); ;
            }
            finally
            {
                connection.Close();
            }
        }

        public T ExecuteScalar<T>(IConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            Events?.OnWriteTrace(Events.IsTraceActive, _logger, "ExecuteScalar Type: {@FullName} Query: {@Text} Parameters: {@parameters}",
                new object[] { typeof(T).FullName!, query.Text, parameters });
            using var command = CreateCommand(connection, query, parameters);
            object? resultCommand = command.ExecuteScalar();
            return (T)SwitchTypeValue(typeof(T), resultCommand)!;
        }

        public async Task<T> ExecuteScalarAsync<T>(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using IConnection connection = await GetConnectionAsync(cancellationToken);
            try
            {
                return await ExecuteScalarAsync<T>(connection, query, parameters, cancellationToken);
            }
            finally
            {
                await connection.CloseAsync(cancellationToken);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(IConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Events?.OnWriteTrace(Events.IsTraceActive, _logger, "ExecuteScalarAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters}",
                new object[] { typeof(T).FullName!, query.Text, parameters });
            using var command = CreateCommand(connection, query, parameters);
            object? resultCommand = await command.ExecuteScalarAsync(cancellationToken);
            return (T)SwitchTypeValue(typeof(T), resultCommand)!;
        }

        public abstract IConnection GetConnection();

        public abstract Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
    }
}
