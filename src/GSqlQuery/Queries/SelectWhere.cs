using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Select where 
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class SelectWhere<T> : BaseWhere<T, SelectQuery<T>>, ISearchCriteriaBuilder<T, SelectQuery<T>>, IWhere<T, SelectQuery<T>>,
        IAndOr<T, SelectQuery<T>> where T : class, new()
    {
        private readonly SelectQueryBuilder<T> _queryBuilder;

        /// <summary>
        /// Initializes a new instance of the SelectWhere class.
        /// </summary>
        /// <param name="queryBuilder">SelectQueryBuilder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SelectWhere(SelectQueryBuilder<T> queryBuilder) : base()
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        /// <summary>
        /// Build Query
        /// </summary>
        /// <returns>DeleteQuery</returns>
        public override SelectQuery<T> Build()
        {
            return _queryBuilder.Build();
        }
    }
}
