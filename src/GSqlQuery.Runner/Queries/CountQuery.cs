using GSqlQuery.Cache;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class CountQuery<T, TDbConnection> : Query<T, ConnectionOptions<TDbConnection>>, IExecute<int, TDbConnection>, IQuery<T>
        where T : class
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        internal CountQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(ref text, table, columns, criteria, connectionOptions)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        public int Execute()
        {
            return DatabaseManagement.ExecuteScalar<int>(this);
        }

        public int Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }

            return DatabaseManagement.ExecuteScalar<int>(dbConnection, this);
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteScalarAsync<int>(this, cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteScalarAsync<int>(dbConnection, this, cancellationToken);
        }
    }
}