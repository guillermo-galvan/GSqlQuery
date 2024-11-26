using GSqlQuery.Cache;
using GSqlQuery.Queries;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class InsertQueryBuilderExecute<T, TDbConnection> : InsertQueryBuilder<T, InsertQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilder<InsertQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public InsertQueryBuilderExecute(ConnectionOptions<TDbConnection> connectionOptions, object entity) : base(connectionOptions, entity)
        { }

        public override InsertQuery<T, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            PropertyOptions property = columns.FirstOrDefault(x => x.Value.ColumnAttribute.IsAutoIncrementing).Value;
            return new InsertQuery<T, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions, _entity, property);
        }
    }
}