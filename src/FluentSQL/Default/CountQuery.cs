namespace FluentSQL.Default
{
    public class CountQuery<T> : Query<T>, ISetDatabaseManagement<long> where T : class, new()
    {
        public CountQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        {
        }

        public IExecute<long, TDbConnection> SetDatabaseManagement<TDbConnection>(IDatabaseManagement<TDbConnection> databaseManagment)
        {
            databaseManagment = databaseManagment ?? throw new ArgumentNullException(nameof(databaseManagment));
            return new CountExecute<TDbConnection, T>(databaseManagment, this);
        }
    }
}
