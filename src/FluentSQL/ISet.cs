﻿using System.Linq.Expressions;

namespace FluentSQL
{
    /// <summary>
    /// Base class to generate the set query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    public interface ISet<T, TReturn> : IBuilder<TReturn> where T : class, new() where TReturn : IQuery<T>
    {
        /// <summary>
        /// Get column values
        /// </summary>
        internal IDictionary<ColumnAttribute, object?> ColumnValues { get; }

        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        IWhere<T, TReturn> Where();
        
        /// <summary>
        /// add to query update another column with value
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property</param>
        /// <param name="value"></param>
        /// <returns>Instance of ISet</returns>
        ISet<T, TReturn> Add<TProperties>( Expression<Func<T, TProperties>> expression, TProperties value);

        /// <summary>
        /// add to query update another column
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        ISet<T, TReturn> Add<TProperties>(Expression<Func<T, TProperties>> expression);
    }
}