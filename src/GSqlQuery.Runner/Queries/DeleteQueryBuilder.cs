using GSqlQuery.Cache;
using System.Collections.Generic;

namespace GSqlQuery.Runner.Queries
{
    internal class DeleteQueryBuilder<T, TDbConnection> : GSqlQuery.Queries.DeleteQueryBuilder<T, DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilder<DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilderWithWhere<T, DeleteQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public DeleteQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions)
        { }

        public DeleteQueryBuilder(object entity, ConnectionOptions<TDbConnection> connectionOptions) : base(entity, connectionOptions)
        {}

        public override DeleteQuery<T, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new DeleteQuery<T, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}