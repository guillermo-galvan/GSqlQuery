using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    internal class CountExecute<TDbConnection, T> : IExecute<long, TDbConnection> where T : class, new()
    {
        private readonly IDatabaseManagement<TDbConnection> _databaseManagment;
        private readonly CountQuery<T> _query;

        public CountExecute(IDatabaseManagement<TDbConnection> databaseManagment, CountQuery<T> query)
        {
            _databaseManagment = databaseManagment;
            _query = query;
        }

        public long Exec()
        {
            return (long)_databaseManagment.ExecuteScalar(_query, _query.GetParameters(_databaseManagment), typeof(long));
        }

        public long Exec(TDbConnection dbConnection)
        {
            return (long)_databaseManagment.ExecuteScalar(dbConnection, _query, _query.GetParameters(_databaseManagment), typeof(long));
        }
    }
}
