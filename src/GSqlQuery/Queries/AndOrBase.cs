using GSqlQuery.Cache;
using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery
{
    /// <summary>
    /// Generate the search criteria
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    /// <exception cref="ArgumentException"></exception>
    public class AndOrBase<T, TReturn, TQueryOptions> : IWhere<TReturn>,
        IAndOr<TReturn>, ISearchCriteriaBuilder, IAndOr<T, TReturn, TQueryOptions>, IWhere<T, TReturn, TQueryOptions>, IQueryOptions<TQueryOptions>,
        IDynamicColumns
        where TReturn : IQuery<T, TQueryOptions>
        where T : class
        where TQueryOptions : QueryOptions
    {
        protected readonly List<ISearchCriteria> _searchCriterias = new List<ISearchCriteria>();
        internal readonly IQueryBuilderWithWhere<TReturn, TQueryOptions> _queryBuilderWithWhere;

        protected PropertyOptionsCollection Columns { get; set; }

        public TQueryOptions QueryOptions { get; }

        public IAndOr<T, TReturn, TQueryOptions> AndOr => Count == 0 ? null : this;

        public IEnumerable<ISearchCriteria> SearchCriterias => _searchCriterias;

        public int Count => _searchCriterias.Count;

        DynamicQuery IDynamicColumns.DynamicQuery
        {
            get 
            {
                if(_queryBuilderWithWhere is IDynamicColumns tmp)
                {
                    return tmp.DynamicQuery;
                }

                return null;
            }
        }

        public AndOrBase(IQueryBuilderWithWhere<TReturn, TQueryOptions> queryBuilderWithWhere, TQueryOptions queryOptions) : base()
        {
            _queryBuilderWithWhere = queryBuilderWithWhere ?? throw new ArgumentNullException(nameof(queryBuilderWithWhere));
            QueryOptions = queryOptions ?? throw new ArgumentNullException(nameof(queryOptions));
            Columns = new PropertyOptionsCollection([]);
        }

        public AndOrBase(IQueryBuilderWithWhere<TReturn, TQueryOptions> queryBuilderWithWhere, TQueryOptions queryOptions, ClassOptions classOptions) : base()
        {
            _queryBuilderWithWhere = queryBuilderWithWhere ?? throw new ArgumentNullException(nameof(queryBuilderWithWhere));
            QueryOptions = queryOptions ?? throw new ArgumentNullException(nameof(queryOptions));
            Columns = classOptions?.PropertyOptions ?? throw new ArgumentNullException(nameof(classOptions));
        }

        /// <summary>
        /// Add search criteria
        /// </summary>
        /// <param name="criteria"></param>
        public void Add(ISearchCriteria criteria)
        {
            _searchCriterias.Add(criteria ?? throw new ArgumentNullException(nameof(criteria), ErrorMessages.ParameterNotNull));
        }

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public virtual IEnumerable<CriteriaDetailCollection> Create()
        {
            CriteriaDetailCollection[] result = new CriteriaDetailCollection[_searchCriterias.Count];
            int count = 0;
            uint parameterId = 0;

            foreach (ISearchCriteria item in _searchCriterias)
            {
                result[count++] = item.GetCriteria(ref parameterId);
            }

            return result;
        }
        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>The query</returns>
        public virtual TReturn Build()
        {
            return _queryBuilderWithWhere.Build();
        }
    }
}