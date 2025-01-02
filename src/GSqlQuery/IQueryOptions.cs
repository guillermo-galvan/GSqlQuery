namespace GSqlQuery
{
    /// <summary>
    /// IQueryOptions
    /// </summary>
    /// <typeparam name="TQueryOptions">Query Options type</typeparam>
    public interface IQueryOptions<TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Get QueryOptions
        /// </summary>
        TQueryOptions QueryOptions { get; }
    }
}