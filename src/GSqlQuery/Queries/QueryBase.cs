using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery
{
    /// <summary>
    /// Query Base
    /// </summary>
    public abstract class QueryBase : IQuery
    {
        private readonly IEnumerable<PropertyOptions> _columns;
        private readonly IEnumerable<CriteriaDetail> _criteria;
        private string _text;

        /// <summary>
        /// Query Text
        /// </summary>
        public string Text { get => _text; set => _text = value; }

        /// <summary>
        /// Get Columns
        /// </summary>
        public IEnumerable<PropertyOptions> Columns => _columns;

        /// <summary>
        /// Get Criterias
        /// </summary>
        public IEnumerable<CriteriaDetail> Criteria => _criteria;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="text">Query text</param>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criterias</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBase(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria)
        {
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));
            text.NullValidate("", nameof(text));
            _text = text;
            _criteria = criteria ?? Enumerable.Empty<CriteriaDetail>();
        }
    }
}