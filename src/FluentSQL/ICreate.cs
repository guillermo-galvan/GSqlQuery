using FluentSQL.Default;

namespace FluentSQL
{
    /// <summary>
    /// Base class to generate the insert query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface ICreate<T> where T : class, new()
    {
        /// <summary>
        /// Generate the insert query,taking into account the name of the first statement collection 
        /// </summary>        
        /// <returns>Instance of IQuery</returns>
        InsertQuery<T> Insert();

        /// <summary>
        /// Generate the insert query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>        
        /// <returns>Instance of IQuery</returns>
        InsertQuery<T> Insert(string key);
    }
}
