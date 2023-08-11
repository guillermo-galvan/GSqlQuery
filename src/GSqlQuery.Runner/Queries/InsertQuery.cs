using GSqlQuery.Extensions;
using GSqlQuery.Runner.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public sealed class InsertQuery<T, TDbConnection> : InsertQuery<T>, IExecute<T, TDbConnection>, IQuery<T>
        where T : class, new()
    {
        public object Entity { get; }

        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        private readonly PropertyOptions _propertyOptionsAutoIncrementing = null;

        internal InsertQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria,
            ConnectionOptions<TDbConnection> connectionOptions, object entity, PropertyOptions propertyOptionsAutoIncrementing)
            : base(text, columns, criteria, connectionOptions.Statements, entity)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _propertyOptionsAutoIncrementing = propertyOptionsAutoIncrementing;
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        private async Task InsertAutoIncrementingAsync(bool isAsync, TDbConnection connection = default, CancellationToken cancellationToken = default)
        {
            object idResult;
            if (connection == null)
            {
                idResult = isAsync ? await DatabaseManagement.ExecuteScalarAsync<object>(this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken)
                                   : DatabaseManagement.ExecuteScalar<object>(this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
            }
            else
            {
                idResult = isAsync ? await DatabaseManagement.ExecuteScalarAsync<object>(connection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken)
                                   : DatabaseManagement.ExecuteScalar<object>(connection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
            }

            var newType = Nullable.GetUnderlyingType(_propertyOptionsAutoIncrementing.PropertyInfo.PropertyType);
            idResult = newType == null ? Convert.ChangeType(idResult, _propertyOptionsAutoIncrementing.PropertyInfo.PropertyType) : Convert.ChangeType(idResult, newType);
            _propertyOptionsAutoIncrementing.PropertyInfo.SetValue(Entity, idResult);
        }

        public T Execute()
        {
            if (_propertyOptionsAutoIncrementing != null)
            {
                InsertAutoIncrementingAsync(false).Wait();
            }
            else
            {
                DatabaseManagement.ExecuteNonQuery(this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
            }

            return (T)Entity;
        }

        public T Execute(TDbConnection dbConnection)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));

            if (_propertyOptionsAutoIncrementing != null)
            {
                InsertAutoIncrementingAsync(false, dbConnection).Wait();
            }
            else
            {
                DatabaseManagement.ExecuteNonQuery(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement));
            }

            return (T)Entity;
        }

        public async Task<T> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            if (_propertyOptionsAutoIncrementing != null)
            {
                await InsertAutoIncrementingAsync(true, cancellationToken: cancellationToken);
            }
            else
            {
                await DatabaseManagement.ExecuteNonQueryAsync(this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
            }

            return (T)Entity;
        }

        public async Task<T> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));

            if (_propertyOptionsAutoIncrementing != null)
            {
                await InsertAutoIncrementingAsync(true, dbConnection, cancellationToken);
            }
            else
            {
                await DatabaseManagement.ExecuteNonQueryAsync(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagement), cancellationToken);
            }

            return (T)Entity;
        }
    }
}