using GSqlQuery.Runner.Transforms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public abstract class DatabaseManagement<TIConnection, TITransaction, TDbCommand, TDbTransaction, TDbDataReader>(string connectionString, DatabaseManagementEvents events)
        : IDatabaseManagement<TIConnection>
        where TIConnection : IConnection<TITransaction, TDbCommand>
        where TITransaction : ITransaction<TIConnection, TDbTransaction>
        where TDbCommand : DbCommand
        where TDbDataReader : DbDataReader
        where TDbTransaction : DbTransaction
    {
        protected readonly string _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

        public DatabaseManagementEvents Events { get; set; } = events ?? throw new ArgumentNullException(nameof(events));

        public string ConnectionString => _connectionString;

        public abstract TIConnection GetConnection();

        public abstract Task<TIConnection> GetConnectionAsync(CancellationToken cancellationToken = default);

        protected static TDbCommand CreateCommand(TIConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            TDbCommand command = connection.GetDbCommand();
            command.CommandText = query.Text;

            if (parameters != null)
            {
                foreach (IDataParameter item in parameters)
                {
                    command.Parameters.Add(item);
                }
            }

            return command;
        }

        public virtual int ExecuteNonQuery(IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using (TIConnection connection = GetConnection())
            {
                try
                {
                    return ExecuteNonQuery(connection, query, parameters); ;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public virtual int ExecuteNonQuery(TIConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            if (Events.IsTraceActive)
            {
                Events.WriteTrace("ExecuteNonQuery Query: {@Text} Parameters: {@parameters}", [query.Text, parameters]);
            }

            using (TDbCommand command = CreateCommand(connection, query, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        public virtual async Task<int> ExecuteNonQueryAsync(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using (TIConnection connection = await GetConnectionAsync(cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return await ExecuteNonQueryAsync(connection, query, parameters, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual Task<int> ExecuteNonQueryAsync(TIConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (Events.IsTraceActive)
            {
                Events.WriteTrace("ExecuteNonQueryAsync Query: {@Text} Parameters: {@parameters}", [query.Text, parameters]);
            }

            using (TDbCommand command = CreateCommand(connection, query, parameters))
            {
                return command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public virtual IEnumerable<T> ExecuteReader<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
            where T : class
        {
            using (TIConnection connection = GetConnection())
            {
                try
                {
                    return ExecuteReader(connection, query, propertyOptions, parameters);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteReader<T>(TIConnection connection, IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters)
            where T : class
        {
            if (Events.IsTraceActive)
            {
                Events.WriteTrace("ExecuteReader Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, parameters]);
            }
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            ITransformTo<T, TDbDataReader> transformToEntity = Events.GetTransformTo<T, TDbDataReader>(classOptions);

            using (TDbCommand command = CreateCommand(connection, query, parameters))
            {
                using (TDbDataReader reader = (TDbDataReader)command.ExecuteReader())
                {
                    return transformToEntity.Transform(propertyOptions, query, reader);
                }
            }
        }

        public virtual async Task<IEnumerable<T>> ExecuteReaderAsync<T>(IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default) where T : class
        {
            using (TIConnection connection = await GetConnectionAsync(cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return await ExecuteReaderAsync(connection, query, propertyOptions, parameters, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual async Task<IEnumerable<T>> ExecuteReaderAsync<T>(TIConnection connection, IQuery<T> query, IEnumerable<PropertyOptions> propertyOptions, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default) where T : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (Events.IsTraceActive)
            {
                Events.WriteTrace("ExecuteReaderAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, parameters]);
            }

            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            ITransformTo<T, TDbDataReader> transformToEntity = Events.GetTransformTo<T, TDbDataReader>(classOptions);

            using (TDbCommand command = CreateCommand(connection, query, parameters))
            {
                using (TDbDataReader reader = (TDbDataReader)await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                {
                    return await transformToEntity.TransformAsync(propertyOptions, query, reader, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual T ExecuteScalar<T>(IQuery query, IEnumerable<IDataParameter> parameters)
        {
            using (TIConnection connection = GetConnection())
            {
                try
                {
                    return ExecuteScalar<T>(connection, query, parameters);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public virtual T ExecuteScalar<T>(TIConnection connection, IQuery query, IEnumerable<IDataParameter> parameters)
        {
            if (Events.IsTraceActive)
            {
                Events.WriteTrace("ExecuteScalar Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, parameters]);
            }

            using (TDbCommand command = CreateCommand(connection, query, parameters))
            {
                object resultCommand = command.ExecuteScalar();
                return (T)TransformTo.SwitchTypeValue(typeof(T), resultCommand);
            }
        }

        public virtual async Task<T> ExecuteScalarAsync<T>(IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            using (TIConnection connection = await GetConnectionAsync(cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return await ExecuteScalarAsync<T>(connection, query, parameters, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual async Task<T> ExecuteScalarAsync<T>(TIConnection connection, IQuery query, IEnumerable<IDataParameter> parameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (Events.IsTraceActive)
            {
                Events.WriteTrace("ExecuteScalarAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, parameters]);
            }

            using (TDbCommand command = CreateCommand(connection, query, parameters))
            {
                object resultCommand = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                return (T)TransformTo.SwitchTypeValue(typeof(T), resultCommand);
            }
        }
    }
}