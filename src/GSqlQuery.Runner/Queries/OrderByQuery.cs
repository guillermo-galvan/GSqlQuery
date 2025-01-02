using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using GSqlQuery.Cache;
using GSqlQuery.Runner;

namespace GSqlQuery
{
    public class OrderByQuery<T, TDbConnection> : Query<T, ConnectionOptions<TDbConnection>>, IExecute<IEnumerable<T>, TDbConnection>, IQuery<T>
        where T : class
    {

        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        internal OrderByQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> connectionOptions) :
            base(ref text, table, columns, criteria, connectionOptions)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        public IEnumerable<T> Execute()
        {
            return DatabaseManagement.ExecuteReader(this, Columns);
        }

        public IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            return DatabaseManagement.ExecuteReader(dbConnection, this, Columns);
        }

        public Task<IEnumerable<T>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteReaderAsync(this, Columns, cancellationToken);
        }

        public Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteReaderAsync(dbConnection, this, Columns, cancellationToken);
        }
    }
}