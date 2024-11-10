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

        public override CountQuery<T, TDbConnection> Build()
        {
            string query = CreateQuery();
            return new CountQuery<T, TDbConnection>(query, Columns, _criteria, QueryOptions);
        }
    }
}