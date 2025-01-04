using GSqlQuery.Cache;
using GSqlQuery.Runner.TypeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
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

        protected TDbCommand CreateCommand(TIConnection connection, IQuery query)
        {
            TDbCommand command = connection.GetDbCommand();
            command.CommandText = query.Text;

            foreach (CriteriaDetailCollection criteriaDetailCollection in query.Criteria?.Where(x => x.Values.Any()) ?? [])
            {
                ITypeHandler<TDbDataReader> typeHandler = Events.GetHandler<TDbDataReader>(criteriaDetailCollection.PropertyOptions.PropertyInfo.PropertyType);

                foreach (ParameterDetail parameterDetail in criteriaDetailCollection.Values)
                {
                    IDataParameter parameter = command.CreateParameter();
                    typeHandler.SetValueDataParameter(parameter, parameterDetail);
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        public virtual int ExecuteNonQuery(IQuery query)
        {
            using (TIConnection connection = GetConnection())
            {
                try
                {
                    return ExecuteNonQuery(connection, query); ;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public virtual int ExecuteNonQuery(TIConnection connection, IQuery query)
        {
            using (TDbCommand command = CreateCommand(connection, query))
            {
                if (Events.IsTraceActive)
                {
                    Events.WriteTrace("ExecuteNonQuery Query: {@Text} Parameters: {@parameters}", [query.Text, command.Parameters]);
                }

                return command.ExecuteNonQuery();
            }
        }

        public virtual async Task<int> ExecuteNonQueryAsync(IQuery query, CancellationToken cancellationToken = default)
        {
            using (TIConnection connection = await GetConnectionAsync(cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return await ExecuteNonQueryAsync(connection, query, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual Task<int> ExecuteNonQueryAsync(TIConnection connection, IQuery query, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (TDbCommand command = CreateCommand(connection, query))
            {
                if (Events.IsTraceActive)
                {
                    Events.WriteTrace("ExecuteNonQueryAsync Query: {@Text} Parameters: {@parameters}", [query.Text, command.Parameters]);
                }

                return command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public virtual IEnumerable<T> ExecuteReader<T>(IQuery<T> query, PropertyOptionsCollection propertyOptions)
            where T : class
        {
            using (TIConnection connection = GetConnection())
            {
                try
                {
                    return ExecuteReader(connection, query, propertyOptions);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteReader<T>(TIConnection connection, IQuery<T> query, PropertyOptionsCollection propertyOptions)
            where T : class
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            ITransformTo<T, TDbDataReader> transformToEntity = Events.GetTransformTo<T, TDbDataReader>(classOptions);

            using (TDbCommand command = CreateCommand(connection, query))
            {
                using (TDbDataReader reader = (TDbDataReader)command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SequentialAccess))
                {
                    if (Events.IsTraceActive)
                    {
                        Events.WriteTrace("ExecuteReader Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, command.Parameters]);
                    }

                    return transformToEntity.Transform(propertyOptions, query, reader, events);
                }
            }
        }

        public virtual async Task<IEnumerable<T>> ExecuteReaderAsync<T>(IQuery<T> query, PropertyOptionsCollection propertyOptions, CancellationToken cancellationToken = default) where T : class
        {
            using (TIConnection connection = await GetConnectionAsync(cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return await ExecuteReaderAsync(connection, query, propertyOptions, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual async Task<IEnumerable<T>> ExecuteReaderAsync<T>(TIConnection connection, IQuery<T> query, PropertyOptionsCollection propertyOptions, CancellationToken cancellationToken = default) where T : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            ITransformTo<T, TDbDataReader> transformToEntity = Events.GetTransformTo<T, TDbDataReader>(classOptions);

            using (TDbCommand command = CreateCommand(connection, query))
            {
                if (Events.IsTraceActive)
                {
                    Events.WriteTrace("ExecuteReaderAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, command.Parameters]);
                }

                using (TDbDataReader reader = (TDbDataReader)await command.ExecuteReaderAsync(CommandBehavior.SingleResult | CommandBehavior.SequentialAccess, cancellationToken).ConfigureAwait(false))
                {
                    return await transformToEntity.TransformAsync(propertyOptions, query, reader, events, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual T ExecuteScalar<T>(IQuery query)
        {
            using (TIConnection connection = GetConnection())
            {
                try
                {
                    return ExecuteScalar<T>(connection, query);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public virtual T ExecuteScalar<T>(TIConnection connection, IQuery query)
        {
            using (TDbCommand command = CreateCommand(connection, query))
            {
                if (Events.IsTraceActive)
                {
                    Events.WriteTrace("ExecuteScalar Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, command.Parameters]);
                }

                object resultCommand = command.ExecuteScalar();
                return (T)GeneralExtension.ConvertToValue(typeof(T), resultCommand);
            }
        }

        public virtual async Task<T> ExecuteScalarAsync<T>(IQuery query, CancellationToken cancellationToken = default)
        {
            using (TIConnection connection = await GetConnectionAsync(cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return await ExecuteScalarAsync<T>(connection, query, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public virtual async Task<T> ExecuteScalarAsync<T>(TIConnection connection, IQuery query, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (TDbCommand command = CreateCommand(connection, query))
            {
                if (Events.IsTraceActive)
                {
                    Events.WriteTrace("ExecuteScalarAsync Type: {@FullName} Query: {@Text} Parameters: {@parameters}", [typeof(T).FullName, query.Text, command.Parameters]);
                }

                object resultCommand = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                return (T)GeneralExtension.ConvertToValue(typeof(T), resultCommand);
            }
        }
    }
}