namespace GSqlQuery
{
    /// <summary>
    /// IOptions
    /// </summary>
    /// <typeparam name="TOptions">Options type</typeparam>
    public interface IOptions<TOptions>
    {
        /// <summary>
        /// Get Options
        /// </summary>
        TOptions Options { get; }
    }
}
