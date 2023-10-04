using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder Base
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    public abstract class QueryBuilderBase<T, TReturn> : IBuilder<TReturn>, IQueryBuilder<TReturn, IFormats>
        where T : class where TReturn : IQuery<T>
    {
        private readonly IFormats _formats;
        protected readonly string _tableName;
        protected readonly ClassOptions _classOptions;

        /// <summary>
        /// Get columns
        /// </summary>
        public IEnumerable<PropertyOptions> Columns { get; protected set; }

        /// <summary>
        /// Get Options
        /// </summary>
        public IFormats Options => _formats;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBuilderBase(IFormats formats)
        {
            _formats = formats ?? throw new ArgumentNullException(nameof(formats));
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            Columns = _classOptions.PropertyOptions;
            _tableName = _classOptions.Table.GetTableName(formats);
        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Query</returns>
        public abstract TReturn Build();
    }
}