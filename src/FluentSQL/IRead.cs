using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// Base class for reading.
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated.</typeparam>
    public interface IRead<T> where T : class, new()
    {
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
        public static IQueryBuilder Select<TProperties>(string key, Expression<Func<T, TProperties>> expression)
        {
            key.NullValidate("Parameter cannot be null or empty", nameof(key));
            expression.NullValidate("Parameter cannot be null", nameof(expression));

            IEnumerable<MemberInfo> memberInfos = expression.GetMembers();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            if (!memberInfos.Any())
            {
                throw new InvalidOperationException($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Select(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Select(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }}) ");
            }

            return new QueryBuilder(options, memberInfos.Select(x => x.Name), FluentSQLManagement._options.StatementsCollection[key]);
        }

        /// <summary>
        /// Defines all properties to perform the query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilder Select(string key)
        {
            key.NullValidate("Parameter cannot be null or empty", nameof(key));
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new QueryBuilder(options, options.PropertyOptions.Select(x => x.PropertyInfo.Name), FluentSQLManagement._options.StatementsCollection[key]);
        }

        /// <summary>
        /// Defines the property or properties to perform the query, taking into account the name of the first statement collection 
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilder Select<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            return Select(FluentSQLManagement._options.StatementsCollection.GetFirstStatements(), expression);
        }

        /// <summary>
        /// Defines the property or properties to perform the query, taking into account the name of the first statement collection 
        /// </summary>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilder Select()
        {
            return Select(FluentSQLManagement._options.StatementsCollection.GetFirstStatements());
        }
    }
}
