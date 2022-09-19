using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Linq.Expressions;

namespace FluentSQL
{
    public abstract class Entity<T> : ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T> where T : class, new()
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
            return IRead<T>.Select(statements, expression);
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
            return IRead<T>.Select(statements);
        }

        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> 
            Select<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            return IRead<T>.Select(connectionOptions, expression);
        }

        public static IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> 
            Select<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            return IRead<T>.Select(connectionOptions);
        }

        /// <summary>
        /// Generate the insert query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>        
        /// <returns>Instance of IQuery</returns>
        public IQueryBuilder<T, InsertQuery<T>> Insert(IStatements statements)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));            
            return new InsertQueryBuilder<T>(statements, this);
        }

        public IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection, T> Insert<TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            return new InsertQueryBuilder<T, TDbConnection>(connectionOptions, this);
        }

        /// <summary>
        /// Generate the update query, taking into account the name of the first statement collection 
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of ISet</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static ISet<T, UpdateQuery<T>> Update<TProperties>(IStatements statements, Expression<Func<T, TProperties>> expression, TProperties value)
        { 
            return IUpdate<T>.Update(statements, expression, value);
        }

        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>> Update<TProperties>(IStatements statements, Expression<Func<T, TProperties>> expression)
        {
            statements.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(statements));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Update(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Update(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new Set<T>(this,memberInfos.Select(x => x.Name), statements);
        }

        public static ISet<T, UpdateQuery<T, TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, 
            Expression<Func<T, TProperties>> expression, TProperties value)
        {
            return IUpdate<T>.Update(connectionOptions, expression, value);
        }
       
        public ISet<T, UpdateQuery<T,TDbConnection>> Update<TProperties, TDbConnection>(ConnectionOptions<TDbConnection> connectionOptions, Expression<Func<T, TProperties>> expression)
        {
            connectionOptions.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(connectionOptions));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Update(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Update(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new SetExecute<T,TDbConnection>(this, memberInfos.Select(x => x.Name), connectionOptions);
        }

        /// <summary>
        /// Generate the delete query, taking into account the name of the first statement collection  
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQueryBuilder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>> Delete(IStatements statements)
        {
            return IDelete<T>.Delete(statements);
        }
    }
}
