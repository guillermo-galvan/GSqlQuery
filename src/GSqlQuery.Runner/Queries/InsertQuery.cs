using GSqlQuery.Cache;
using GSqlQuery.Runner;
using GSqlQuery.Runner.TypeHandles;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public sealed class InsertQuery<T, TDbConnection> : Query<T, ConnectionOptions<TDbConnection>>, IExecute<T, TDbConnection>, IQuery<T>
        where T : class
    {
        public object Entity { get; }

        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        private readonly PropertyOptions _propertyOptionsAutoIncrementing = null;


        internal InsertQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> connectionOptions, object entity, PropertyOptions propertyOptionsAutoIncrementing)
            : base(ref text, table, columns, criteria, connectionOptions)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _propertyOptionsAutoIncrementing = propertyOptionsAutoIncrementing;
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        private async Task InsertAutoIncrementingAsync(TDbConnection connection = default, CancellationToken cancellationToken = default)
        {
            object idResult;
            if (connection == null)
            {
                idResult = await DatabaseManagement.ExecuteScalarAsync<object>(this, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                idResult = await DatabaseManagement.ExecuteScalarAsync<object>(connection, this, cancellationToken).ConfigureAwait(false);
            }

            idResult = GeneralExtension.ConvertToValue(_propertyOptionsAutoIncrementing.PropertyInfo.PropertyType, idResult);
            _propertyOptionsAutoIncrementing.PropertyInfo.SetValue(Entity, idResult);
        }

        private void InsertAutoIncrementing(TDbConnection connection = default)
        {
            object idResult;
            if (connection == null)
            {
                idResult = DatabaseManagement.ExecuteScalar<object>(this);
            }
            else
            {
                idResult = DatabaseManagement.ExecuteScalar<object>(connection, this);
            }

            idResult = GeneralExtension.ConvertToValue(_propertyOptionsAutoIncrementing.PropertyInfo.PropertyType, idResult);
            _propertyOptionsAutoIncrementing.PropertyInfo.SetValue(Entity, idResult);
        }

        public T Execute()
        {
            if (_propertyOptionsAutoIncrementing != null)
            {
                InsertAutoIncrementing();
            }
            else
            {
                DatabaseManagement.ExecuteNonQuery(this);
            }

            return (T)Entity;
        }

        public T Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }

            if (_propertyOptionsAutoIncrementing != null)
            {
                InsertAutoIncrementing(dbConnection);
            }
            else
            {
                DatabaseManagement.ExecuteNonQuery(dbConnection, this);
            }

            return (T)Entity;
        }

        public async Task<T> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (_propertyOptionsAutoIncrementing != null)
            {
                await InsertAutoIncrementingAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await DatabaseManagement.ExecuteNonQueryAsync(this, cancellationToken).ConfigureAwait(false);
            }

            return (T)Entity;
        }

        public async Task<T> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            cancellationToken.ThrowIfCancellationRequested();
            if (_propertyOptionsAutoIncrementing != null)
            {
                await InsertAutoIncrementingAsync(dbConnection, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await DatabaseManagement.ExecuteNonQueryAsync(dbConnection, this, cancellationToken).ConfigureAwait(false);
            }

            return (T)Entity;
        }
    }
}