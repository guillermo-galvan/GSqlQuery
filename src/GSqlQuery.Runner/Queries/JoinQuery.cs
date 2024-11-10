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

        internal JoinQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(text, columns, criteria, connectionOptions)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
        }

        public IEnumerable<T> Execute()
        {
            return DatabaseManagement.ExecuteReader(this, Columns,
                GeneralExtension.GetParameters<T, TDbConnection>(this, DatabaseManagement));
        }

        public IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            return DatabaseManagement.ExecuteReader(dbConnection, this, Columns,
                GeneralExtension.GetParameters<T, TDbConnection>(this, DatabaseManagement));
        }

        public Task<IEnumerable<T>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return DatabaseManagement.ExecuteReaderAsync(this, Columns,
                GeneralExtension.GetParameters<T, TDbConnection>(this, DatabaseManagement), cancellationToken);
        }

        public Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            return DatabaseManagement.ExecuteReaderAsync(dbConnection, this, Columns,
                GeneralExtension.GetParameters<T, TDbConnection>(this, DatabaseManagement), cancellationToken);
        }
    }
}