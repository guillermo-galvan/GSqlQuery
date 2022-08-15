namespace FluentSQL
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQuery<T> where T : class, new()
    {
        /// <summary>
        /// Columns of the query
        /// </summary>
        IEnumerable<ColumnAttribute> Columns { get; }

        /// <summary>
        /// Query criteria
        /// </summary>
        IEnumerable<CriteriaDetail>? Criteria { get; }

        /// <summary>
        /// Statements to use in the query
        /// </summary>
        IStatements Statements { get; }

        /// <summary>
        /// The Query
        /// </summary>
        string Text { get; }
    }
}
