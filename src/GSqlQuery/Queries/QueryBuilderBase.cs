﻿using GSqlQuery.Cache;
using System;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder Base
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    public abstract class QueryBuilderBase<T, TReturn, TQueryOptions> : IBuilder<TReturn>, IQueryBuilder<TReturn, TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected readonly string _tableName;
        protected readonly ClassOptions _classOptions;

        /// <summary>
        /// Get columns
        /// </summary>
        public PropertyOptionsCollection Columns { get; protected set; }

        /// <summary>
        /// Query Options
        /// </summary>
        public TQueryOptions QueryOptions { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBuilderBase(TQueryOptions queryOptions, PropertyOptionsCollection columns = null)
        {
            QueryOptions = queryOptions ?? throw new ArgumentNullException(nameof(queryOptions));
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            Columns = columns ?? _classOptions.PropertyOptions;
            _tableName = _classOptions.FormatTableName.GetTableName(QueryOptions.Formats);
        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Query</returns>
        public abstract TReturn Build();
    }
}