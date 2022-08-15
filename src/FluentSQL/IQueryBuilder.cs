namespace FluentSQL
{
    /// <summary>
    /// Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQueryBuilder<T> where T : class, new()
    {
        /// <summary>
        /// Statements to use in the query
        /// </summary>
        IStatements Statements { get; }

        /// <summary>
        /// Build Query
        /// </summary>
        IQuery<T> Build();

        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        IWhere<T> Where();
    }
}
