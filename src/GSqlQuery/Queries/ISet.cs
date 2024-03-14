﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Set 
    /// </summary>
    public interface ISet
    {
        /// <summary>
        /// Get column values
        /// </summary>
        IDictionary<ColumnAttribute, object> ColumnValues { get; }
    }

    /// <summary>
    /// Generate the set query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface ISet<T, TReturn, TQueryOptions> : IBuilder<TReturn>, ISet, IQueryBuilderWithWhere<TReturn, TQueryOptions>, IQueryBuilderWithWhere<T, TReturn, TQueryOptions>, IQueryOptions<TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// add to query update another column with value
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property</param>
        /// <param name="value"></param>
        /// <returns>Instance of ISet</returns>
        ISet<T, TReturn, TQueryOptions> Set<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value);

        /// <summary>
        /// add to query update another column
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        ISet<T, TReturn, TQueryOptions> Set<TProperties>(Expression<Func<T, TProperties>> expression);
    }
}