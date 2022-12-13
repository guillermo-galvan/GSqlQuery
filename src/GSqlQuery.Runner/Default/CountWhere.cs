using GSqlQuery.Default;
using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Runner.Default
{
    internal class CountWhere<T, TDbConnection> : BaseWhere<T, CountQuery<T, TDbConnection>>, ISearchCriteriaBuilder<T, CountQuery<T, TDbConnection>>,
        IWhere<T, CountQuery<T, TDbConnection>>,
        IAndOr<T, CountQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly CountQueryBuilder<T, TDbConnection> _queryBuilder;

        public CountWhere(CountQueryBuilder<T, TDbConnection> queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public override CountQuery<T, TDbConnection> Build()
        {
            return _queryBuilder.Build();
        }
    }
}
