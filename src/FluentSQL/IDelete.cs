using FluentSQL.Default;
using FluentSQL.Extensions;

namespace FluentSQL
{
    /// <summary>
    /// Base class to generate the delete query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface IDelete<T> where T : class, new()
    {
        /// <summary>
        /// Generate the delete query, taking into account the name of the first statement collection  
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQueryBuilder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>> Delete(IStatements statements)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            return new DeleteQueryBuilder<T>(statements);
        }
    }
}
