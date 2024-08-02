using GSqlQuery.SearchCriteria;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Contains the details of the criteria
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the CriteriaDetailCollection class.
    /// </remarks>
    /// <param name="queryPart">Query part</param>
    /// /// <param name="propertyOptions">PropertyOptions</param>
    /// <exception cref="ArgumentNullException"></exception>
    public sealed class CriteriaDetailCollection(string queryPart, PropertyOptions propertyOptions) : IEnumerable<KeyValuePair<string, ParameterDetail>>
    {
        private readonly Dictionary<string, ParameterDetail> _keyValues = [];

        /// <summary>
        /// Get PropertyOptions
        /// </summary>
        public PropertyOptions PropertyOptions { get; } = propertyOptions ?? throw new ArgumentNullException(nameof(propertyOptions));

        /// <summary>
        /// Get Query part 
        /// </summary>
        public string QueryPart { get; } = queryPart ?? throw new ArgumentNullException(nameof(queryPart));

        public IEnumerable<string> Keys => _keyValues.Keys;
        
        public IEnumerable<ParameterDetail> Values => _keyValues.Values;

        public int Count => _keyValues.Count;

        /// <summary>
        /// Get Search Criteria
        /// </summary>
        internal ISearchCriteria SearchCriteria { get; }

        public ParameterDetail this[string key]
        {
            get
            {
                return _keyValues[key];
            }
            internal set
            {
                if (_keyValues.ContainsKey(key))
                {
                    throw new InvalidOperationException("The key already exists");
                }
                _keyValues[key] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CriteriaDetail class.
        /// </summary>
        /// <param name="searchCriteria">Search Criteria</param>
        /// <param name="queryPart">Query part</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CriteriaDetailCollection(ISearchCriteria searchCriteria, string queryPart, PropertyOptions propertyOptions) : this(queryPart, propertyOptions)
        {
            SearchCriteria = searchCriteria ?? throw new ArgumentNullException(nameof(searchCriteria));
        }

        internal CriteriaDetailCollection(ISearchCriteria searchCriteria, string queryPart, PropertyOptions propertyOptions, ParameterDetail[] parameters) : this(searchCriteria,queryPart, propertyOptions)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            DataFill(parameters);
        }

        internal CriteriaDetailCollection(string queryPart, PropertyOptions propertyOptions, ParameterDetail[] parameters) : this(queryPart, propertyOptions)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            DataFill(parameters);
        }

        private void DataFill(ParameterDetail[] parameters)
        {
            foreach (ParameterDetail item in parameters)
            {
                this[item.Name] = item;
            }
        }

        public IEnumerator<KeyValuePair<string, ParameterDetail>> GetEnumerator()
        {
            foreach (KeyValuePair<string, ParameterDetail> item in _keyValues)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}