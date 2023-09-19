namespace GSqlQuery
{
    public interface IQueryBuilderWithWhere<T, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>
        where T : class
        where TReturn : IQuery<T>
    {
        new IWhere<T, TReturn> Where();
    }
}