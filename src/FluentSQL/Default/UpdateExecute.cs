using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    internal class UpdateExecute<TDbConnection, T> : IExecute<int, TDbConnection> where T : class, new()
    {
        private readonly IDatabaseManagement<TDbConnection> _databaseManagment;
        private readonly UpdateQuery<T> _query;

        public UpdateExecute(IDatabaseManagement<TDbConnection> databaseManagment, UpdateQuery<T> query)
        {
            _databaseManagment = databaseManagment;
            _query = query;
        }

        public int Exec()
        {
            return _databaseManagment.ExecuteNonQuery(_query, _query.GetParameters<T, TDbConnection>(_databaseManagment));
        }

        public int Exec(TDbConnection dbConnection)
        {
            return _databaseManagment.ExecuteNonQuery(dbConnection,_query, _query.GetParameters<T, TDbConnection>(_databaseManagment));
        }
    }
}
