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
}
