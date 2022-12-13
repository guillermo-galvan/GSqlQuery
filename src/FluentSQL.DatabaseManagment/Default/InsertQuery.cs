using FluentSQL.DatabaseManagement.Extensions;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.DatabaseManagement.Default
{
    public class InsertQuery<T, TDbConnection> : Query<T, TDbConnection, T>, IQuery<T, TDbConnection, T>,
        IExecute<T, TDbConnection> where T : class, new()
    {
        public object Entity { get; }
        protected PropertyOptions? _propertyOptionsAutoIncrementing = null;

        public InsertQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions,
            object entity, PropertyOptions? propertyOptionsAutoIncrementing) :
            base(text, columns, criteria, connectionOptions)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _propertyOptionsAutoIncrementing = propertyOptionsAutoIncrementing;
        }

        private async Task InsertAutoIncrementingAsync(bool isAsync, TDbConnection? connection = default, CancellationToken cancellationToken = default)
        {
            object idResult;
            if (connection == null)
            {
                idResult = isAsync ? await DatabaseManagment.ExecuteScalarAsync<object>(this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken)
                                   : DatabaseManagment.ExecuteScalar<object>(this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
            }
            else
            {
                idResult = isAsync ? await DatabaseManagment.ExecuteScalarAsync<object>(connection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken)
                                   : DatabaseManagment.ExecuteScalar<object>(connection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
            }

            var newType = Nullable.GetUnderlyingType(_propertyOptionsAutoIncrementing!.PropertyInfo.PropertyType);
            idResult = newType == null ? Convert.ChangeType(idResult, _propertyOptionsAutoIncrementing!.PropertyInfo.PropertyType) : Convert.ChangeType(idResult, newType);
            _propertyOptionsAutoIncrementing.PropertyInfo.SetValue(this.Entity, idResult);
        }
        
        public override T Execute()
        {
            if (_propertyOptionsAutoIncrementing != null)
            {
                InsertAutoIncrementingAsync(false).Wait();
            }
            else
            {
                DatabaseManagment.ExecuteNonQuery(this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
            }

            return (T)Entity;
        }

        public override T Execute(TDbConnection dbConnection)
        {
            dbConnection!.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));

            if (_propertyOptionsAutoIncrementing != null)
            {
                InsertAutoIncrementingAsync(false,dbConnection).Wait();
            }
            else
            {
                DatabaseManagment.ExecuteNonQuery(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
            }

            return (T)Entity;
        }

        public override async Task<T> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            if (_propertyOptionsAutoIncrementing != null)
            {
                await InsertAutoIncrementingAsync(true,cancellationToken:cancellationToken);
            }
            else
            {
                await DatabaseManagment.ExecuteNonQueryAsync(this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
            }

            return (T)Entity;
        }

        public override async Task<T> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            dbConnection!.NullValidate(ErrorMessages.ParameterNotNull, nameof(dbConnection));

            if (_propertyOptionsAutoIncrementing != null)
            {
                await InsertAutoIncrementingAsync(true,dbConnection, cancellationToken);
            }
            else
            {
                await DatabaseManagment.ExecuteNonQueryAsync(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
            }

            return (T)Entity;
        }
    }
}
