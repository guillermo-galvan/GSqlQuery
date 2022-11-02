namespace FluentSQL
{
    public interface IQueryBuilderWithWhere<T, TReturn> : IQueryBuilder<T, TReturn>, IBuilder<TReturn> where T : class, new() where TReturn : IQuery<T>
    {
        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        IWhere<T, TReturn> Where();
    }

    public interface IQueryBuilderWithWhere<T, TReturn, TDbConnection, TResult> : IQueryBuilder<T, TReturn, TDbConnection, TResult>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery<T, TDbConnection, TResult>
    {
        IWhere<T, TReturn, TDbConnection, TResult> Where();
    }
}
