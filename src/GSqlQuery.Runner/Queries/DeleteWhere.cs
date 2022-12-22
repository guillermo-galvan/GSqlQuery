using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Runner.Queries
{
    internal class DeleteWhere<T, TDbConnection> : BaseWhere<T, DeleteQuery<T, TDbConnection>>, ISearchCriteriaBuilder<T, DeleteQuery<T, TDbConnection>>,
        IWhere<T, DeleteQuery<T, TDbConnection>>,
        IAndOr<T, DeleteQuery<T, TDbConnection>> where T : class, new()
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
