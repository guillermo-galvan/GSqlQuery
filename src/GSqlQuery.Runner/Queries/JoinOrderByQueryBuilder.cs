using GSqlQuery.Cache;
using GSqlQuery.Queries;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.Runner.Queries
{
    internal class JoinOrderByQueryBuilder<T, TDbConnection> : JoinOrderByQueryBuilder<T, OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, JoinQuery<T, TDbConnection>>, IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public JoinOrderByQueryBuilder(Expression expression, OrderBy orderBy, IQueryBuilderWithWhere<T, JoinQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder)
            : base(expression, orderBy, queryBuilder, queryBuilder.QueryOptions)
        { }

        public JoinOrderByQueryBuilder(Expression expression, OrderBy orderBy, IAndOr<T, JoinQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> andOr, ConnectionOptions<TDbConnection> options)
           : base(expression, orderBy, andOr, options)
        { }

        public override OrderByQuery<T, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new OrderByQuery<T, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}