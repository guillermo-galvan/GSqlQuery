namespace FluentSQL.DatabaseManagement
{
    public interface IQuery<T, TDbConnection, TResult> : IQuery<T>, IQuery, IExecute<TResult, TDbConnection> where T : class, new()
    {
        public new IDatabaseManagement<TDbConnection> DatabaseManagment { get; }
    }
}
