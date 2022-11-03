using FluentSQL.Helpers;
using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Delete where
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class DeleteWhere<T> : BaseWhere<T, DeleteQuery<T>>, ISearchCriteriaBuilder<T, DeleteQuery<T>>, IWhere<T, DeleteQuery<T>>, IAndOr<T, DeleteQuery<T>> where T : class, new()
    {
        private readonly DeleteQueryBuilder<T> _queryBuilder;

        /// <summary>
        /// Initializes a new instance of the DeleteWhere class.
        /// </summary>
        /// <param name="queryBuilder">DeleteQueryBuilder</param>
        /// <exception cref="ArgumentNullException"></exception>

        public DeleteWhere(DeleteQueryBuilder<T> queryBuilder) : base()
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        /// <summary>
        /// Build Query
        /// </summary>
        /// <returns>DeleteQuery</returns>
        public override DeleteQuery<T> Build()
        {
            return _queryBuilder.Build();
        }

        /// <summary>
        /// Build the criteria
        /// </summary>
        /// <returns>Criteria detail enumerable</returns>
        public override IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            return _searchCriterias.Select(x => x.GetCriteria(statements, Columns)).ToArray();
        }
    }

    internal class DeleteWhere<T, TDbConnection> : BaseWhere<T, DeleteQuery<T, TDbConnection>>, ISearchCriteriaBuilder<T, DeleteQuery<T, TDbConnection>>,       
        IWhere<T, DeleteQuery<T, TDbConnection>>,
        IAndOr<T,DeleteQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly DeleteQueryBuilder<T, TDbConnection> _queryBuilder;

        public DeleteWhere(DeleteQueryBuilder<T, TDbConnection> queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public override DeleteQuery<T, TDbConnection> Build()
        {
            return _queryBuilder.Build();
        }
    }
}
