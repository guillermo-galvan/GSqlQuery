using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

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
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new DeleteQueryBuilder<T>(options, options.PropertyOptions.Select(x => x.PropertyInfo.Name), statements);
        }
    }
}
