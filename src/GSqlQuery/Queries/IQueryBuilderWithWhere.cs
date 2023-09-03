namespace GSqlQuery
{
    public interface IQueryBuilderWithWhere<T, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>
        where T : class, new()
        where TReturn : IQuery<T>
    {
        new IWhere<T, TReturn> Where();
    }
}