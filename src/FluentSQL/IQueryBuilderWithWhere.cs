namespace FluentSQL
{
    public interface IQueryBuilderWithWhere<T, TReturn> : IQueryBuilder<T, TReturn>, IBuilder<TReturn> where T : class, new() where TReturn : IQuery
    {
        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        IWhere<T, TReturn> Where();
    }

    public interface IQueryBuilderWithWhere<T, TReturn, TDbConnection> : IQueryBuilderWithWhere<T, TReturn> , IQueryBuilder<T, TReturn, TDbConnection>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {   
    }
}
