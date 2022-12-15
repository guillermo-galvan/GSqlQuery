using GSqlQuery.Extensions;
using GSqlQuery.Queries;

namespace GSqlQuery
{
    /// <summary>
    /// Base class to generate the insert query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface ICreate<T> where T : class, new()
    {
        /// <summary>
        /// Generate the insert query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQuery</returns>
        IQueryBuilder<T, InsertQuery<T>> Insert(IStatements statements);
    }
}
