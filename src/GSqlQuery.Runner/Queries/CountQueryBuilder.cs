namespace GSqlQuery.Runner.Queries
{
    internal class CountQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, CountQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhereRunner<T, CountQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderRunner<T, CountQuery<T, TDbConnection>, TDbConnection>, IBuilder<CountQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly IQueryBuilder<T, SelectQuery<T, TDbConnection>> _queryBuilder;

        public CountQueryBuilder(IQueryBuilder<T, SelectQuery<T, TDbConnection>> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions)
        {
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
            _andOr = null;
        }

        public override CountQuery<T, TDbConnection> Build()
        {
            IQuery selectQuery = _queryBuilder.Build();
            var query = GSqlQuery.Queries.CountQueryBuilder<T>.CreateQuery(_andOr != null, Statements, selectQuery.Columns, _tableName, _andOr != null ? GetCriteria() : string.Empty);
            return new CountQuery<T, TDbConnection>(query, selectQuery.Columns, _criteria, ConnectionOptions);
        }

        public override IWhere<T, CountQuery<T, TDbConnection>> Where()
        {
            _andOr = new AndOrBase<T, CountQuery<T, TDbConnection>>(this);
            return (IWhere<T, CountQuery<T, TDbConnection>>)_andOr;
        }
    }
}
