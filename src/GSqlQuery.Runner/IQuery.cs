namespace GSqlQuery
{
    public interface IQuery<T, TDbConnection, TResult> : IQuery<T>, IQuery, IExecute<TResult, TDbConnection> where T : class, new()
    {
        new IDatabaseManagement<TDbConnection> DatabaseManagment { get; }
    }
}
