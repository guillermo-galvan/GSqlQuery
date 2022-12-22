namespace GSqlQuery
{
    public interface IQueryBuilder<T, TReturn, TDbConnection> : IQueryBuilder<T, TReturn>, IBuilder<TReturn>
        where T : class, new() where
        TReturn : IQuery
    {
        ConnectionOptions<TDbConnection> ConnectionOptions { get; }
    }
}
