namespace GSqlQuery
{
    /// <summary>
    /// Base class to generate the insert query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface ICreate<T> where T : class
    {
        /// <summary>
        /// Generate the insert query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQuery</returns>
        IQueryBuilder<InsertQuery<T>, IStatements> Insert(IStatements statements);
    }
}