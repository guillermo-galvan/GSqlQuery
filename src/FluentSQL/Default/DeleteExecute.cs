using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    internal class DeleteExecute<TDbConnection, T> : IExecute<int, TDbConnection> where T : class, new()
    {
        private readonly IDatabaseManagement<TDbConnection> _databaseManagment;
        private readonly DeleteQuery<T> _query;

        public DeleteExecute(IDatabaseManagement<TDbConnection> databaseManagment, DeleteQuery<T> query)
        {
            _databaseManagment = databaseManagment ?? throw new ArgumentNullException(nameof(databaseManagment));
            _query = query;
        }

        public int Exec()
        {
            return _databaseManagment.ExecuteNonQuery(_query, _query.GetParameters<T, TDbConnection>(_databaseManagment));
        }

        public int Exec(TDbConnection dbConnection)
        {
            return _databaseManagment.ExecuteNonQuery(dbConnection, _query, _query.GetParameters<T, TDbConnection>(_databaseManagment));
        }
    }
}
