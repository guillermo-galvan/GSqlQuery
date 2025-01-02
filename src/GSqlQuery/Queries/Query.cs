using GSqlQuery.Cache;
using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query 
    /// </summary>
    public abstract class Query : IQuery
    {
        private readonly PropertyOptionsCollection _columns;
        private readonly IEnumerable<CriteriaDetailCollection> _criteria;
        private string _text;

        /// <summary>
        /// Get Table
        /// </summary>
        public TableAttribute Table { get; }

        /// <summary>
        /// Query Text
        /// </summary>
        public string Text { get => _text; }

        /// <summary>
        /// Get Columns
        /// </summary>
        public PropertyOptionsCollection Columns => _columns;

        /// <summary>
        /// Get Criterias
        /// </summary>
        public IEnumerable<CriteriaDetailCollection> Criteria => _criteria;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query text</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criterias</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(ref string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));
            _text = text ?? throw new ArgumentNullException(text);
            _criteria = criteria ?? [];
        }
    }

    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Query<T, TQueryOptions> : Query, IQuery<T, TQueryOptions>
        where T : class
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Get QueryOptions
        /// </summary>
        public TQueryOptions QueryOptions { get; }

        /// <summary>
        /// Create Query object 
        /// </summary>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="formats">Formats</param>
        /// <param name="text">The Query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(ref string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions) :
            base(ref text, table, columns, criteria)
        {
            QueryOptions = queryOptions ?? throw new ArgumentNullException(nameof(queryOptions));
        }
    }
}