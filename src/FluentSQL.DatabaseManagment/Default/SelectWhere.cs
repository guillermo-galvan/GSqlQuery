using FluentSQL.Default;
using FluentSQL.SearchCriteria;

namespace FluentSQL.DatabaseManagement.Default
{
    internal class SelectWhere<T, TDbConnection> : BaseWhere<T, SelectQuery<T, TDbConnection>>, ISearchCriteriaBuilder<T, SelectQuery<T, TDbConnection>>,
        IWhere<T, SelectQuery<T, TDbConnection>>,
        IAndOr<T, SelectQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly SelectQueryBuilder<T, TDbConnection> _selectQueryBuilder;

        public SelectWhere(SelectQueryBuilder<T, TDbConnection> selectQueryBuilder) : base()
        {
            _selectQueryBuilder = selectQueryBuilder;
        }

        public override SelectQuery<T, TDbConnection> Build()
        {
            return _selectQueryBuilder.Build();
        }
    }
}
