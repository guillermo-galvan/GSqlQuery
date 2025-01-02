using GSqlQuery.Cache;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.Runner.Queries
{
    internal class UpdateQueryBuilder<T, TDbConnection> : GSqlQuery.Queries.UpdateQueryBuilder<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilder<UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        ISet<T, UpdateQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity, Expression expression) :
             base(connectionOptions, entity, expression)
        { }

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, Expression expression, object value) :
            base(connectionOptions, expression, value)
        { }

        public override UpdateQuery<T, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new UpdateQuery<T, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}