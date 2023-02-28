using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class DeleteQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, DeleteQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhereRunner<T, DeleteQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderRunner<T, DeleteQuery<T, TDbConnection>, TDbConnection>, IBuilder<DeleteQuery<T, TDbConnection>>
        where T : class, new()
    {
        public DeleteQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions)
        {
        }

        public override DeleteQuery<T, TDbConnection> Build()
        {
            var query = GSqlQuery.Queries.DeleteQueryBuilder<T>.CreateQuery(_andOr != null, Options.Statements, _tableName, _andOr != null ? GetCriteria() : string.Empty);
            return new DeleteQuery<T, TDbConnection>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Options);
        }

        public override IWhere<T, DeleteQuery<T, TDbConnection>> Where()
        {
            _andOr = new AndOrBase<T,DeleteQuery<T, TDbConnection>>(this);
            return (IWhere<T, DeleteQuery<T, TDbConnection>>)_andOr;
        }
    }
}
