namespace GSqlQuery
{
    /// <summary>
    /// Query Builder With Where
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options Type</typeparam>
    public interface IQueryBuilderWithWhere<T, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>
        where T : class
        where TReturn : IQuery<T>
    {
        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="T"/>, <typeparamref name="TReturn"/>&gt;</returns>
        new IWhere<T, TReturn> Where();
    }
}