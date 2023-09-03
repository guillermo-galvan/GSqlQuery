using GSqlQuery.Runner.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public class BatchExecute<TDbConnection> : IExecute<int, TDbConnection>
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;
        private readonly Queue<IQuery> _queries;
        private readonly Queue<IDataParameter> _parameters;
        private readonly StringBuilder _queryBuilder;
        private readonly Queue<PropertyOptions> _columns;

        public ConnectionOptions<TDbConnection> DatabaseManagement => _connectionOptions;

        IDatabaseManagement<TDbConnection> IExecute<int, TDbConnection>.DatabaseManagement => _connectionOptions.DatabaseManagement;

        internal BatchExecute(ConnectionOptions<TDbConnection> connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _queries = new Queue<IQuery>();
            _parameters = new Queue<IDataParameter>();
            _queryBuilder = new StringBuilder();
            _columns = new Queue<PropertyOptions>();
        }

        public BatchExecute<TDbConnection> Add<T>(Func<ConnectionOptions<TDbConnection>, IQuery<T>> expression)
            where T : class, new()
        {
            IQuery query = expression.Invoke(_connectionOptions);
            foreach (var item in query.GetParameters<T, TDbConnection>(_connectionOptions.DatabaseManagement))
            {
                _parameters.Enqueue(item);
            }

            _queryBuilder.Append(query.Text);

            foreach (var item in query.Columns)
            {
                _columns.Enqueue(item);
            }

            _queries.Enqueue(query);
            return this;
        }

        public int Execute()
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQuery(query, _parameters);
        }

        public int Execute(TDbConnection connection)
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQuery(connection, query, _parameters);
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQueryAsync(query, _parameters, cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection connection, CancellationToken cancellationToken = default)
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagement.ExecuteNonQueryAsync(connection, query, _parameters, cancellationToken);
        }
    }
}