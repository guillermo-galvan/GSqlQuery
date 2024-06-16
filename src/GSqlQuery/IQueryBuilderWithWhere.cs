namespace GSqlQuery
{
    public interface IQueryBuilderWithWhere<TReturn, TQueryOptions> : IQueryBuilder<TReturn, TQueryOptions>, IBuilder<TReturn>, IQueryOptions<TQueryOptions>
        where TReturn : IQuery
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        IWhere<TReturn> Where();
    }

    /// <summary>
    /// Query Builder With Where
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options Type</typeparam>
    public interface IQueryBuilderWithWhere<T, TReturn, TQueryOptions> : IQueryBuilderWithWhere<TReturn, TQueryOptions>, IQueryOptions<TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="T"/>, <typeparamref name="TReturn"/>&gt;</returns>
        new IWhere<T, TReturn, TQueryOptions> Where();
    }
}