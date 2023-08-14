using GSqlQuery.Queries;
using System.Collections.Generic;

namespace GSqlQuery.Runner.Queries
{
    internal class OrderByQueryBuilder<T, TDbConnection> : OrderByQueryBuilder<T, OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, SelectQuery<T, TDbConnection>>,
        IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class, new()
    {
        new public ConnectionOptions<TDbConnection> Options { get; }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder)
            : base(selectMember, orderBy, queryBuilder, queryBuilder.Options.Statements)
        {
            Options = queryBuilder.Options;
        }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
           IAndOr<T, SelectQuery<T, TDbConnection>> andOr, ConnectionOptions<TDbConnection> options)
           : base(selectMember, orderBy, andOr, options.Statements)
        {
            Options = options;
        }

        public override OrderByQuery<T, TDbConnection> Build()
        {
            var query = CreateQuery(Options.Statements, out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T, TDbConnection>(query, columns, criteria, Options);
        }
    }
}