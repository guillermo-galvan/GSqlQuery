namespace GSqlQuery
{
    /// <summary>
    /// Helper for de join query
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options type</typeparam>
    public interface IComparisonOperators<T, TReturn, TOptions> : IOptions<TOptions>
        where TReturn : IQuery<T>
        where T : class
    {

    }
}