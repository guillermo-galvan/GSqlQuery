using GSqlQuery.Runner;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class JoinQuery<T, TDbConnection>: Query<T, TDbConnection, IEnumerable<T>>, IQueryRunner<T, TDbConnection, IEnumerable<T>>,
        IExecuteDatabaseManagement<IEnumerable<T>, TDbConnection> where T : class, new ()
    {
        internal JoinQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions) :
            base(text, columns, criteria, connectionOptions)
        {
        }

        public override IEnumerable<T> Execute()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<T>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
