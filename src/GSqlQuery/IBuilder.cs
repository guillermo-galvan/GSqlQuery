namespace GSqlQuery
{
    /// <summary>
    /// Builder
    /// </summary>
    /// <typeparam name="TReturn">Query</typeparam>
    public interface IBuilder<TReturn> where TReturn : IQuery
    {
        /// <summary>
        /// Build query
        /// </summary>
        /// <returns>Query</returns>
        TReturn Build();
    }
}