using FluentSQL.Helpers;
using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Select where 
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class SelectWhere<T> : BaseWhere<T>, ISearchCriteriaBuilder, IWhere<T, SelectQuery<T>>, IAndOr<T, SelectQuery<T>> where T : class, new()
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
        public SelectQuery<T> Build()
        {
            return _queryBuilder.Build();
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        public override IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            return _searchCriterias.Select(x => x.GetCriteria(statements, Columns)).ToArray();
        }
    }
}
