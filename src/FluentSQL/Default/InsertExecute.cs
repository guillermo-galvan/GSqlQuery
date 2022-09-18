using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    internal class InsertExecute<TDbConnection, T> : IExecute<T, TDbConnection> where T : class, new()
    {
        private readonly IDatabaseManagement<TDbConnection> _databaseManagment;
        private readonly InsertQuery<T> _query;

        public InsertExecute(IDatabaseManagement<TDbConnection> databaseManagment, InsertQuery<T> query)
        {
            _databaseManagment = databaseManagment;
            _query = query;
        }

        private void InsertAutoIncrementing(TDbConnection? connection = default)
        {
            var classOptions = _query.GetClassOptions();
            var columnAutoIncrementing = _query.Columns.First(x => x.IsAutoIncrementing);
            var propertyOptions = classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == columnAutoIncrementing.Name);

            object idResult;
            if (connection == null)
            {
                idResult = _databaseManagment.ExecuteScalar(_query, _query.GetParameters<T, TDbConnection>(_databaseManagment),
                    propertyOptions.PropertyInfo.PropertyType);
            }
            else
            {
                idResult = _databaseManagment.ExecuteScalar(connection, _query, _query.GetParameters<T, TDbConnection>(_databaseManagment),
                    propertyOptions.PropertyInfo.PropertyType);
            }

            propertyOptions.PropertyInfo.SetValue(_query.Entity, idResult);
        }

        public T Exec()
        {
            if (_query.Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing();
            }
            else
            {
                _databaseManagment.ExecuteNonQuery(_query, _query.GetParameters<T, TDbConnection>(_databaseManagment));
            }

            return (T)_query.Entity;
        }

        public T Exec(TDbConnection dbConnection)
        {
            if (_query.Columns.Any(x => x.IsAutoIncrementing))
            {
                InsertAutoIncrementing(dbConnection);
            }
            else
            {
                _databaseManagment.ExecuteNonQuery(dbConnection, _query, _query.GetParameters<T, TDbConnection>(_databaseManagment));
            }

            return (T)_query.Entity;
        }
    }
}
