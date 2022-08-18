﻿using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// Base class to generate the update query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface IUpdate<T> where T : class, new()
    {
        /// <summary>
        /// Generate the update query, taking into account the name of the first statement collection 
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        ISet<T> Update<TProperties>(Expression<Func<T, TProperties>> expression);

        /// <summary>
        /// Generate the update query
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="key">The name of the statement collection</param>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        ISet<T> Update<TProperties>(string key, Expression<Func<T, TProperties>> expression);

        /// <summary>
        /// Generate the update query, taking into account the name of the first statement collection 
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <param name="value">Value</param>
        /// <returns>Instance of ISet</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static ISet<T> Update<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            return Update(FluentSQLManagement._options.StatementsCollection.GetFirstStatements(), expression, value);
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
        public static ISet<T> Update<TProperties>(string key,Expression<Func<T, TProperties>> expression, TProperties value)
        {
            key.NullValidate(ErrorMessages.ParameterNotNullEmpty, nameof(key));
            var (options, memberInfos) = expression.GetOptionsAndMember();
            memberInfos.ValidateMemberInfo(options);
            return new Set<T>(options, new string[] { memberInfos.Name }, FluentSQLManagement._options.StatementsCollection[key], value);
        }
    }
}
