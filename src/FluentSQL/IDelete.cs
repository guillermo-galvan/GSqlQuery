using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// Base class to generate the delete query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface IDelete<T> where T : class, new()
    {
        /// <summary>
        /// Generate the delete query
        /// </summary>
        /// <returns>Instance of IQueryBuilder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>> Delete()
        {
            return Delete(FluentSQLManagement._options.ConnectionCollection.GetFirstStatements());
        }

        /// <summary>
        /// Generate the delete query, taking into account the name of the first statement collection  
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQueryBuilder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>> Delete(string key)
        {
            key.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(key));
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new DeleteQueryBuilder<T>(options, options.PropertyOptions.Select(x => x.PropertyInfo.Name), FluentSQLManagement._options.ConnectionCollection[key]);
        }
    }
}
