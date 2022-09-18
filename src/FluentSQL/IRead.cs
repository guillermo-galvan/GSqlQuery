using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Linq.Expressions;

namespace FluentSQL
{
    /// <summary>
    /// Base class for reading.
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
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
        public static IQueryBuilderWithWhere<T, SelectQuery<T>> Select<TProperties>(IStatements statements, Expression<Func<T, TProperties>> expression)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Select(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Select(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new SelectQueryBuilder<T>(memberInfos.Select(x => x.Name), statements);
        }

        /// <summary>
        /// Defines all properties to perform the query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilderWithWhere<T, SelectQuery<T>> Select(IStatements statements)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            return new SelectQueryBuilder<T>(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions.Select(x => x.PropertyInfo.Name), statements);
        }

        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> 
            Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, 
            Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Select(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Select(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new SelectQueryBuilder<T,TDbConnection>(memberInfos.Select(x => x.Name), connectionOptions);
        }

        public static IQueryBuilderWithWhere<T, SelectQuery<T,TDbConnection>,TDbConnection, IEnumerable<T>> 
            Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new SelectQueryBuilder<T, TDbConnection>(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions.Select(x => x.PropertyInfo.Name), connectionOptions);
        }
    }
}
