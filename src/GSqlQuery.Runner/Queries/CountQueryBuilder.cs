using GSqlQuery.Cache;
using System.Collections.Generic;

namespace GSqlQuery.Runner.Queries
{
    internal class CountQueryBuilder<T, TDbConnection> :
        GSqlQuery.Queries.CountQueryBuilder<T, CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, SelectQuery<T, TDbConnection>>,
        IQueryBuilder<CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilderWithWhere<T, CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {

        public CountQueryBuilder(IQueryBuilderWithWhere<SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder) :
            base(queryBuilder, queryBuilder.QueryOptions)
        { }

        public override CountQuery<T, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new CountQuery<T, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}