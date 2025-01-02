using GSqlQuery.Cache;
using GSqlQuery.Queries;
using System.Collections.Generic;

namespace GSqlQuery.Runner.Queries
{
    internal class OrderByQueryBuilder<T, TDbConnection> : OrderByQueryBuilder<T, OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, SelectQuery<T, TDbConnection>>,
        IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {

        public OrderByQueryBuilder(DynamicQuery dynamicQuery, OrderBy orderBy, IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder) : base(dynamicQuery, orderBy, queryBuilder, queryBuilder.QueryOptions)
        { }

        public OrderByQueryBuilder(DynamicQuery dynamicQuery, OrderBy orderBy,
           IAndOr<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> andOr)
           : base(dynamicQuery, orderBy, andOr)
        { }

        public override OrderByQuery<T, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new OrderByQuery<T, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}