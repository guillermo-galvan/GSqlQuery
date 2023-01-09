namespace GSqlQuery
{
    public interface IQuery<T, TDbConnection, TResult> : IQuery<T>, IQuery, IExecuteDatabaseManagement<TResult, TDbConnection> where T : class, new()
    {
        new IDatabaseManagement<TDbConnection> DatabaseManagement { get; }
    }
}
