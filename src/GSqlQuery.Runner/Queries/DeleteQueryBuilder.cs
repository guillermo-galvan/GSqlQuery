using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class DeleteQueryBuilder<T, TDbConnection> : GSqlQuery.Queries.DeleteQueryBuilder<T, DeleteQuery<T, TDbConnection>>,
        IQueryBuilder<DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class, new()
    {
        new public ConnectionOptions<TDbConnection> Options { get; }

        public DeleteQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions.Statements)
        {
            Options = connectionOptions;
        }

        public override DeleteQuery<T, TDbConnection> Build()
        {
            return new DeleteQuery<T, TDbConnection>(CreateQuery(Options.Statements), Columns, _criteria, Options);
        }

        IWhere<DeleteQuery<T, TDbConnection>> IQueryBuilderWithWhere<DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.Where()
        {
            return Where();
        }
    }
}