using GSqlQuery.Runner;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IEnumerable<IDataParameter> _parameters;


        internal InsertQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria,
            ConnectionOptions<TDbConnection> connectionOptions, object entity, PropertyOptions propertyOptionsAutoIncrementing)
            : base(ref text, columns, criteria, connectionOptions)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _propertyOptionsAutoIncrementing = propertyOptionsAutoIncrementing;
            DatabaseManagement = connectionOptions.DatabaseManagement;
            _parameters = GeneralExtension.GetParameters<T, TDbConnection>(this, DatabaseManagement);
        }

        private async Task InsertAutoIncrementingAsync(TDbConnection connection = default, CancellationToken cancellationToken = default)
        {
            object idResult;
            if (connection == null)
            {
                idResult = await DatabaseManagement.ExecuteScalarAsync<object>(this, _parameters, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                idResult = await DatabaseManagement.ExecuteScalarAsync<object>(connection, this, _parameters, cancellationToken).ConfigureAwait(false);
            }

            Type newType = Nullable.GetUnderlyingType(_propertyOptionsAutoIncrementing.PropertyInfo.PropertyType);
            idResult = newType == null ? Convert.ChangeType(idResult, _propertyOptionsAutoIncrementing.PropertyInfo.PropertyType) : Convert.ChangeType(idResult, newType);
            _propertyOptionsAutoIncrementing.PropertyInfo.SetValue(Entity, idResult);
        }

        private void InsertAutoIncrementing(TDbConnection connection = default)
        {
            object idResult;
            if (connection == null)
            {
                idResult = DatabaseManagement.ExecuteScalar<object>(this, _parameters);
            }
            else
            {
                idResult = DatabaseManagement.ExecuteScalar<object>(connection, this, _parameters);
            }

            Type newType = Nullable.GetUnderlyingType(_propertyOptionsAutoIncrementing.PropertyInfo.PropertyType);
            idResult = newType == null ? Convert.ChangeType(idResult, _propertyOptionsAutoIncrementing.PropertyInfo.PropertyType) : Convert.ChangeType(idResult, newType);
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
                DatabaseManagement.ExecuteNonQuery(this, _parameters);
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
                DatabaseManagement.ExecuteNonQuery(dbConnection, this, _parameters);
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
                await DatabaseManagement.ExecuteNonQueryAsync(this, _parameters, cancellationToken).ConfigureAwait(false);
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
                await DatabaseManagement.ExecuteNonQueryAsync(dbConnection, this, _parameters, cancellationToken).ConfigureAwait(false);
            }

            return (T)Entity;
        }
    }
}