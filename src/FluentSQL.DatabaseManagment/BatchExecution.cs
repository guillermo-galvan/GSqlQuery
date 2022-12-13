using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Extensions;
using FluentSQL.DatabaseManagement.Models;
using System.Data;
using System.Text;

namespace FluentSQL.DatabaseManagement
{
    public class BatchExecute<TDbConnection> : IExecute<int, TDbConnection>
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;
        private readonly Queue<IQuery> _queries;
        private readonly Queue<IDataParameter> _parameters;
        private readonly StringBuilder _queryBuilder;
        private readonly Queue<ColumnAttribute> _columns;

        public ConnectionOptions<TDbConnection> DatabaseManagment => _connectionOptions;

        IDatabaseManagement<TDbConnection> IExecute<int, TDbConnection>.DatabaseManagment => _connectionOptions.DatabaseManagment;

        public BatchExecute(ConnectionOptions<TDbConnection> connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            _queries = new Queue<IQuery>();
            _parameters = new Queue<IDataParameter>();
            _queryBuilder = new();
            _columns = new();
        }

        public BatchExecute<TDbConnection> Add<T, TResult>(Func<ConnectionOptions<TDbConnection>, IQuery<T, TDbConnection, TResult>> expression)
            where T : class, new()
        {
            IQuery query = expression.Invoke(_connectionOptions);
            foreach (var item in query.GetParameters<T, TDbConnection>(_connectionOptions.DatabaseManagment))
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
            return _connectionOptions.DatabaseManagment.ExecuteNonQuery(query, _parameters);
        }

        public int Execute(TDbConnection connection)
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagment.ExecuteNonQuery(connection, query, _parameters);
        }

        public Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagment.ExecuteNonQueryAsync(query, _parameters, cancellationToken);
        }

        public Task<int> ExecuteAsync(TDbConnection connection, CancellationToken cancellationToken = default)
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagment.ExecuteNonQueryAsync(connection, query, _parameters, cancellationToken);
        }
    }
}
