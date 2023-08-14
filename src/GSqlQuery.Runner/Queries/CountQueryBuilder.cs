using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class CountQueryBuilder<T, TDbConnection> :
        GSqlQuery.Queries.CountQueryBuilder<T, CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, SelectQuery<T, TDbConnection>>,
        IQueryBuilder<CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilderWithWhere<T, CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class, new()
    {
        new public ConnectionOptions<TDbConnection> Options { get; }

        public CountQueryBuilder(IQueryBuilderWithWhere<SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder) :
            base(queryBuilder, queryBuilder.Options.Statements)
        {
            Options = queryBuilder.Options;
        }

        public override CountQuery<T, TDbConnection> Build()
        {
            var query = CreateQuery(Options.Statements);
            return new CountQuery<T, TDbConnection>(query, Columns, _criteria, _queryBuilder.Options);
        }

        IWhere<CountQuery<T, TDbConnection>> IQueryBuilderWithWhere<CountQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.Where()
        {
            return Where();
        }
    }
}