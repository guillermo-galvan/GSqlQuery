using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Linq.Expressions;

namespace FluentSQL
{
    public abstract class Entity<T> : IRead<T>, ICreate<T> where T : class, new()
    {
        /// <summary>
        /// Defines the property or properties to perform the query, taking into account the name of the first statement collection 
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilder<T> Select<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            return IRead<T>.Select(expression);
        }

        /// <summary>
        /// Defines the property or properties to perform the query, taking into account the name of the first statement collection 
        /// </summary>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilder<T> Select()
        {
            return IRead<T>.Select();
        }

        /// <summary>
        /// Defines the property or properties to perform the query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilder<T> Select<TProperties>(string key, Expression<Func<T, TProperties>> expression)
        {
            return IRead<T>.Select(key, expression);
        }

        /// <summary>
        /// Defines all properties to perform the query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilder<T> Select(string key)
        {
            return IRead<T>.Select(key);
        }

        /// <summary>
        /// Generate the insert query,taking into account the name of the first statement collection 
        /// </summary>        
        /// <returns>Instance of IQuery</returns>
        public IQuery<T> Insert()
        {
            return Insert(FluentSQLManagement._options.StatementsCollection.GetFirstStatements());
        }

        /// <summary>
        /// Generate the insert query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>        
        /// <returns>Instance of IQuery</returns>
        public IQuery<T> Insert(string key)
        {
            key.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(key));
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new QueryBuilder<T>(options, options.PropertyOptions.Select(x => x.PropertyInfo.Name), FluentSQLManagement._options.StatementsCollection[key], QueryType.Insert, this).Build();
        }
    }
}
