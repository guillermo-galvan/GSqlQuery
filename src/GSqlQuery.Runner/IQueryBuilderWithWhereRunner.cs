namespace GSqlQuery
{
    public interface IQueryBuilderWithWhereRunner<T, TReturn, TDbConnection> : IQueryBuilderWithWhere<T, TReturn>, IQueryBuilderRunner<T, TReturn, TDbConnection>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {
    }
}
