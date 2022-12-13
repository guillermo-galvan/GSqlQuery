using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Default
{
    /// <summary>
    /// Update where 
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateWhere<T> : BaseWhere<T, UpdateQuery<T>>, ISearchCriteriaBuilder<T, UpdateQuery<T>>, IWhere<T, UpdateQuery<T>>, IAndOr<T, UpdateQuery<T>> where T : class, new()
    {
        private readonly UpdateQueryBuilder<T> _queryBuilder;

        /// <summary>
        /// Initializes a new instance of the UpdateWhere class.
        /// </summary>
        /// <param name="queryBuilder">UpdateQueryBuilder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateWhere(UpdateQueryBuilder<T> queryBuilder) : base()
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override UpdateQuery<T> Build()
        {
            return _queryBuilder.Build();
        }
    }
}
