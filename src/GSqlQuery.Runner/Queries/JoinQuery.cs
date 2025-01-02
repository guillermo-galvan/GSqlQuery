using GSqlQuery.Cache;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public class JoinQuery<T, TDbConnection> : GSqlQuery.JoinQuery<T, ConnectionOptions<TDbConnection>>, IExecute<IEnumerable<T>, TDbConnection>, IQuery<T>, IQuery<T, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        internal JoinQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> connectionOptions, TableAttribute secondTable)
            : base(text, table, columns, criteria, connectionOptions, secondTable)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        internal JoinQuery(string text, TableAttribute table, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> connectionOptions, TableAttribute secondTable, TableAttribute thirdTable)
           : base(text, table, columns, criteria, connectionOptions, secondTable, thirdTable)
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
            return DatabaseManagement.ExecuteReaderAsync(this, Columns, cancellationToken);
        }

        public Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            return DatabaseManagement.ExecuteReaderAsync(dbConnection, this, Columns, cancellationToken);
        }
    }
}