using GSqlQuery.Cache;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class DeleteQuery<T, TDbConnection> : Query<T, ConnectionOptions<TDbConnection>>, IExecute<int, TDbConnection>
         where T : class
    {
        internal DeleteQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> connectionOptions) :
            base(ref text, table, columns, criteria, connectionOptions)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        public int Execute()
        {
            return DatabaseManagement.ExecuteNonQuery(this);
        }

        public int Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            return DatabaseManagement.ExecuteNonQuery(dbConnection, this);
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteNonQueryAsync(this, cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteNonQueryAsync(dbConnection, this, cancellationToken);
        }
    }
}