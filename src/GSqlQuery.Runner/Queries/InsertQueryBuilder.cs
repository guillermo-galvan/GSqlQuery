using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class InsertQueryBuilder<T, TDbConnection> : QueryBuilderBase<T, InsertQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderRunner<T, InsertQuery<T, TDbConnection>, TDbConnection>,
        IBuilder<InsertQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly object _entity;

        public InsertQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity)
            : base(connectionOptions)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public override InsertQuery<T, TDbConnection> Build()
        {
            IEnumerable<CriteriaDetail> criteria = null;
            var query = InsertQueryBuilder<T>.CreateQuery(Statements, Columns, _tableName, _entity, ref criteria);
            return new InsertQuery<T, TDbConnection>(query, Columns.Select(x => x.ColumnAttribute), criteria, ConnectionOptions, _entity, Columns.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
        }
    }
}
