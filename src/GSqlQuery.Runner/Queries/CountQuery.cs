using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery
{
    public class CountQuery<T, TDbConnection> : Query<T, ConnectionOptions<TDbConnection>>, IExecute<int, TDbConnection>, IQuery<T>
        where T : class
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        private readonly IEnumerable<IDataParameter> _parameters;

        internal CountQuery(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(ref text, columns, criteria, connectionOptions)
        {
            DatabaseManagement = connectionOptions.DatabaseManagement;
            _parameters = Runner.GeneralExtension.GetParameters<T, TDbConnection>(this, DatabaseManagement);
        }

        public int Execute()
        {
            return DatabaseManagement.ExecuteScalar<int>(this, _parameters);
        }

        public int Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }

            return DatabaseManagement.ExecuteScalar<int>(dbConnection, this, _parameters);
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteScalarAsync<int>(this, _parameters, cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection), ErrorMessages.ParameterNotNull);
            }
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagement.ExecuteScalarAsync<int>(dbConnection, this, _parameters, cancellationToken);
        }
    }
}