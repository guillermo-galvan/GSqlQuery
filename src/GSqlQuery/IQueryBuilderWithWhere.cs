namespace GSqlQuery
{
    public interface IQueryBuilderWithWhere<TReturn, TOptions> : IQueryBuilder<TReturn, TOptions>, IBuilder<TReturn>, IOptions<TOptions>
        where TReturn : IQuery
    {
        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        IWhere<TReturn> Where();
    }
}