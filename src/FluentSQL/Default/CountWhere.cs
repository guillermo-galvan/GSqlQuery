using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    internal class CountWhere<T> : BaseWhere<T>, ISearchCriteriaBuilder, IWhere<T, CountQuery<T>>, IAndOr<T, CountQuery<T>> where T : class, new()
    {
        private readonly CountQueryBuilder<T> _queryBuilder;

        public CountWhere(CountQueryBuilder<T> queryBuilder) : base()
        {
            _queryBuilder = queryBuilder;
        }

        public CountQuery<T> Build()
        {
            return _queryBuilder.Build();
        }
    }

    internal class CountWhere<T, TDbConnection> : BaseWhere<T>, ISearchCriteriaBuilder,
        IWhere<T, CountQuery<T, TDbConnection>, TDbConnection>,
        IWhere<T, CountQuery<T, TDbConnection>>,
        IAndOr<T, CountQuery<T, TDbConnection>, TDbConnection>,
        IAndOr<CountQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly CountQueryBuilder<T, TDbConnection> _queryBuilder;

        public CountWhere(CountQueryBuilder<T, TDbConnection> queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public CountQuery<T, TDbConnection> Build()
        {
            return _queryBuilder.Build();
        }
    }
}
