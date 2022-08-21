using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Update where 
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateWhere<T> : BaseWhere, ISearchCriteriaBuilder, IWhere<T, UpdateQuery<T>>, IAndOr<T, UpdateQuery<T>> where T : class, new()
    {
        private readonly UpdateQueryBuilder<T> _queryBuilder;

        /// <summary>
        /// Initializes a new instance of the UpdateWhere class.
        /// </summary>
        /// <param name="queryBuilder">UpdateQueryBuilder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateWhere(UpdateQueryBuilder<T> queryBuilder)
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UpdateQuery<T> Build()
        {
            return _queryBuilder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<CriteriaDetail> BuildCriteria()
        {
            return _searchCriterias.Select(x => x.GetCriteria(_queryBuilder.ConnectionOptions.Statements)).ToArray();
        }
    }
}
