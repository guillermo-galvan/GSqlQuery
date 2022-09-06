namespace FluentSQL.Default
{
    internal class BatchQueryExecute<TDbConnection> : IExecute<int, TDbConnection>
    {
        private readonly IDatabaseManagement<TDbConnection> _databaseManagment;
        private readonly BatchQuery _query;

        public BatchQueryExecute(IDatabaseManagement<TDbConnection> databaseManagement, BatchQuery query)
        {
            _databaseManagment = databaseManagement;
            _query = query;
        }

        public int Exec()
        {
            return _databaseManagment.ExecuteNonQuery(_query, _query.Parameters);
        }

        public int Exec(TDbConnection dbConnection)
        {
            return _databaseManagment.ExecuteNonQuery(dbConnection, _query, _query.Parameters);
        }
    }
}
