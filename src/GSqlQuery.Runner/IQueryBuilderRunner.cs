namespace GSqlQuery
{
    public interface IQueryBuilderRunner<T, TReturn, TDbConnection> : IQueryBuilder<T, TReturn>, IBuilder<TReturn>
        where T : class, new() where
        TReturn : IQuery
    {
        new ConnectionOptions<TDbConnection> Options { get; }
    }
}
