using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>

    internal class Where<T> : ISearchCriteriaBuilder, IWhere<T>, IAndOr<T> where T : class, new()
    {
        private readonly IQueryBuilder<T> _queryBuilder;
        private List<ISearchCriteria> _searchCriterias = new();

        /// <summary>
        /// Create IQuery object 
        /// </summary>
        /// <param name="queryBuilder">QueryBuilder</param>
        public Where(IQueryBuilder<T> queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Add a search criteria
        /// </summary>
        /// <param name="criteria"></param>
        public void Add(ISearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            _searchCriterias.Add(criteria);
        }

        /// <summary>
        /// Build Query
        /// </summary>
        public IQuery<T> Build()
        {
           return _queryBuilder.Build();
        }

        /// <summary>
        /// Build the criteria
        /// </summary>
        /// <returns>Criteria detail enumerable</returns>
        IEnumerable<CriteriaDetail> ISearchCriteriaBuilder.BuildCriteria()
        {
            return _searchCriterias.Select(x => x.GetCriteria(_queryBuilder.Statements)).ToList();
        }
    }
}
