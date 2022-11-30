using FluentSQL.DatabaseManagement.Extensions;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;

namespace FluentSQL.DatabaseManagement.Default
{
    public class InsertQuery<T, TDbConnection> : Query<T, TDbConnection, T>, IQuery<T, TDbConnection, T>,
        IExecute<T, TDbConnection> where T : class, new()
    {
        public object Entity { get; }

        public InsertQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions<TDbConnection> connectionOptions,
            object entity) :
            base(text, columns, criteria, connectionOptions)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        private void InsertAutoIncrementing(TDbConnection? connection = default)
        {
            var classOptions = GetClassOptions();
            var columnAutoIncrementing = Columns.First(x => x.IsAutoIncrementing);
            var propertyOptions = classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == columnAutoIncrementing.Name);

            object idResult;
            if (connection == null)
            {
                idResult = DatabaseManagment.ExecuteScalar<object>(this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
            }
            else
            {
                idResult = DatabaseManagment.ExecuteScalar<object>(connection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
            }

            var newType = Nullable.GetUnderlyingType(propertyOptions.PropertyInfo.PropertyType);
            idResult = newType == null ? Convert.ChangeType(idResult, propertyOptions.PropertyInfo.PropertyType) : Convert.ChangeType(idResult, newType);

            propertyOptions.PropertyInfo.SetValue(this.Entity, idResult);
        }

        private async Task InsertAutoIncrementingAsync(CancellationToken cancellationToken, TDbConnection? connection = default)
        {
            var classOptions = GetClassOptions();
            var columnAutoIncrementing = Columns.First(x => x.IsAutoIncrementing);
            var propertyOptions = classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == columnAutoIncrementing.Name);

            object idResult;
            if (connection == null)
            {
                idResult = await DatabaseManagment.ExecuteScalarAsync<object>(this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
            }
            else
            {
                idResult = await DatabaseManagment.ExecuteScalarAsync<object>(connection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
            }

            var newType = Nullable.GetUnderlyingType(propertyOptions.PropertyInfo.PropertyType);
            idResult = newType == null ? Convert.ChangeType(idResult, propertyOptions.PropertyInfo.PropertyType) : Convert.ChangeType(idResult, newType);

            propertyOptions.PropertyInfo.SetValue(this.Entity, idResult);
        }

        public override T Execute()
        {
            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing();
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

            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing(dbConnection);
            }
            else
            {
                DatabaseManagment.ExecuteNonQuery(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment));
            }

            return (T)Entity;
        }

        public override async Task<T> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                await InsertAutoIncrementingAsync(cancellationToken);
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

            if (Columns.Any(x => x.IsAutoIncrementing))
            {
                await InsertAutoIncrementingAsync(cancellationToken, dbConnection);
            }
            else
            {
                await DatabaseManagment.ExecuteNonQueryAsync(dbConnection, this, this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
            }

            return (T)Entity;
        }
    }
}
