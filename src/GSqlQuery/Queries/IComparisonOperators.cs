namespace GSqlQuery
{
    /// <summary>
    /// Helper for de join query
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    public interface IComparisonOperators<T, TReturn, TQueryOptions> : IQueryOptions<TQueryOptions>
        where TReturn : IQuery<T, TQueryOptions>
        where T : class
        where TQueryOptions : QueryOptions
    {

    }
}