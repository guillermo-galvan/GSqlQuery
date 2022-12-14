using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Runner.Default
{
    internal class UpdateWhere<T, TDbConnection> : BaseWhere<T, UpdateQuery<T, TDbConnection>>, ISearchCriteriaBuilder<T, UpdateQuery<T, TDbConnection>>,
        IWhere<T, UpdateQuery<T, TDbConnection>>,
        IAndOr<T, UpdateQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly UpdateQueryBuilder<T, TDbConnection> _queryBuilder;

        public UpdateWhere(UpdateQueryBuilder<T, TDbConnection> queryBuilder) : base()
        {
            _queryBuilder = queryBuilder;
        }

        public override UpdateQuery<T, TDbConnection> Build()
        {
            return _queryBuilder.Build();
        }
    }
}
