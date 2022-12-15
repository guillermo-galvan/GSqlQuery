namespace GSqlQuery.Runner.Queries
{
    internal class DeleteQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, DeleteQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, DeleteQuery<T, TDbConnection>, TDbConnection>, IBuilder<DeleteQuery<T, TDbConnection>>
        where T : class, new()
    {
        public DeleteQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions, QueryType.Delete)
        {
        }

        public override DeleteQuery<T, TDbConnection> Build()
        {
            return new DeleteQuery<T, TDbConnection>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions);
        }

        public override IWhere<T, DeleteQuery<T, TDbConnection>> Where()
        {
            ChangeQueryType();
            _andOr = new DeleteWhere<T, TDbConnection>(this);
            return (IWhere<T, DeleteQuery<T, TDbConnection>>)_andOr;
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_queryType == QueryType.Delete)
            {
                result = string.Format(ConnectionOptions.Statements.Delete, _tableName);
            }
            else if (_queryType == QueryType.DeleteWhere)
            {
                result = string.Format(ConnectionOptions.Statements.DeleteWhere, _tableName, GetCriteria());
            }

            return result;
        }
    }
}
