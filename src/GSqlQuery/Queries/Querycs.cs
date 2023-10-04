using System;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Query<T> : QueryBase, IQuery<T> where T : class
    {
        private readonly ClassOptions _classOptions;

        /// <summary>
        /// Get Formats
        /// </summary>
        public IFormats Formats { get; }

        /// <summary>
        /// Get class options
        /// </summary>
        /// <returns>class options</returns>
        protected virtual ClassOptions GetClassOptions()
        {
            return _classOptions;
        }

        /// <summary>
        /// Create Query object 
        /// </summary>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="formats">Formats</param>
        /// <param name="text">The Query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, IFormats formats) :
            base(text, columns, criteria)
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
            Formats = formats ?? throw new ArgumentNullException(nameof(formats));
        }
    }
}