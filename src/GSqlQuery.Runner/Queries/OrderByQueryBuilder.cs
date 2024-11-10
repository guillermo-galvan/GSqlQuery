using GSqlQuery.Queries;
using System.Collections.Generic;
using System.Reflection;
using GSqlQuery.Extensions;

namespace GSqlQuery.Runner.Queries
{
    internal class OrderByQueryBuilder<T, TDbConnection> : OrderByQueryBuilder<T, OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, SelectQuery<T, TDbConnection>>,
        IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {

        public OrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla, OrderBy orderBy,IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder) : base(classOptionsTupla, orderBy, queryBuilder, queryBuilder.QueryOptions)
        { }

        public OrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla, OrderBy orderBy,
           IAndOr<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> andOr)
           : base(classOptionsTupla, orderBy, andOr)
        { }

        public override OrderByQuery<T, TDbConnection> Build()
        {
            string query = CreateQuery(out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T, TDbConnection>(query, columns, criteria, QueryOptions);
        }
    }
}