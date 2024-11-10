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

        public override InsertQuery<T, TDbConnection> Build()
        {
            string query = CreateQuery(out IEnumerable<CriteriaDetail> criteria);
            PropertyOptions property = Columns.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing);
            return new InsertQuery<T, TDbConnection>(query, Columns, criteria, QueryOptions, _entity, property);
        }
    }
}