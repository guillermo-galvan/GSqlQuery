﻿using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Linq.Expressions;

namespace FluentSQL
{
    public abstract class Entity<T> : ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T> where T : class, new()
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
        public static IQueryBuilderWithWhere<T, SelectQuery<T>> Select<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            return IRead<T>.Select(expression);
        }

        /// <summary>
        /// Defines the property or properties to perform the query, taking into account the name of the first statement collection 
        /// </summary>
        /// <returns>Instance of IQueryBuilder with which to create the query</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IQueryBuilderWithWhere<T, SelectQuery<T>> Select()
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
        public static IQueryBuilderWithWhere<T, SelectQuery<T>> Select<TProperties>(string key, Expression<Func<T, TProperties>> expression)
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
        public static IQueryBuilderWithWhere<T, SelectQuery<T>> Select(string key)
        {
            return IRead<T>.Select(key);
        }

        /// <summary>
        /// Generate the insert query,taking into account the name of the first statement collection 
        /// </summary>        
        /// <returns>Instance of IQuery</returns>
        public InsertQuery<T> Insert()
        {
            return Insert(FluentSQLManagement._options.ConnectionCollection.GetFirstStatements());
        }

        /// <summary>
        /// Generate the insert query
        /// </summary>
        /// <param name="key">The name of the statement collection</param>        
        /// <returns>Instance of IQuery</returns>
        public InsertQuery<T> Insert(string key)
        {
            key.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(key));
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));
            return new InsertQueryBuilder<T>(options, options.PropertyOptions.Select(x => x.PropertyInfo.Name), FluentSQLManagement._options.ConnectionCollection[key], this).Build();
        }

        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of ISet</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static ISet<T, UpdateQuery<T>> Update<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            return IUpdate<T>.Update(expression, value);
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
        public static ISet<T, UpdateQuery<T>> Update<TProperties>(string key, Expression<Func<T, TProperties>> expression, TProperties value)
        { 
            return IUpdate<T>.Update(key, expression, value);
        }

        /// <summary>
        /// Generate the update query, taking into account the name of the first statement collection 
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>> Update<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            return Update(FluentSQLManagement._options.ConnectionCollection.GetFirstStatements(), expression);
        }

        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>> Update<TProperties>(string key, Expression<Func<T, TProperties>> expression)
        {
            key.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(key));
            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Update(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Update(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");
            return new Set<T>(this,options, memberInfos.Select(x => x.Name), FluentSQLManagement._options.ConnectionCollection[key]);
        }

        /// <summary>
        /// Generate the delete query
        /// </summary>
        /// <returns>Instance of IQueryBuilder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>> Delete()
        {
            return IDelete<T>.Delete();
        }

        /// <summary>
        /// Generate the delete query, taking into account the name of the first statement collection  
        /// </summary>
        /// <param name="key">The name of the statement collection</param>
        /// <returns>Instance of IQueryBuilder</returns>
        public static IQueryBuilderWithWhere<T, DeleteQuery<T>> Delete(string key)
        {
            return IDelete<T>.Delete(key);
        }
    }
}
