namespace FluentSQL
{
    /// <summary>
    /// Query Builder
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        /// Get Columns of the query
        /// </summary>
        IEnumerable<ColumnAttribute> Columns { get; }

        /// <summary>
        /// Build Query
        /// </summary>
        string Build();
    }
}
