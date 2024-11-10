using GSqlQuery.Runner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class SelectQuery<T, TDbConnection> : Query<T, ConnectionOptions<TDbConnection>>, IExecute<IEnumerable<T>, TDbConnection>, IQuery<T>
        where T : class
    {
        private readonly IEnumerable<IDataParameter> _parameters;

        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        internal SelectQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(ref text, columns, criteria, connectionOptions)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
            _parameters = GeneralExtension.GetParameters<T, TDbConnection>(this, DatabaseManagement);
        }

        public IEnumerable<T> Execute()
        {
            return DatabaseManagement.ExecuteReader(this, Columns, _parameters);
        }

        public IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            return DatabaseManagement.ExecuteReader(dbConnection, this, Columns, _parameters);
        }

        public Task<IEnumerable<T>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteReaderAsync(this, Columns, _parameters, cancellationToken);
        }

        public Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteReaderAsync(dbConnection, this, Columns, _parameters, cancellationToken);
        }
    }
}