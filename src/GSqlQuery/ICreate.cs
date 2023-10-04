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
        /// <returns>Instance of IQuery</returns>
        IQueryBuilder<InsertQuery<T>, IFormats> Insert(IFormats formats);
    }
}