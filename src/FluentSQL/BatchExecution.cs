using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using System.Data;
using System.Text;

namespace FluentSQL
{
    public class BatchExecute<TDbConnection>
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;
        private readonly Queue<IQuery> _queries;
        private readonly List<IDataParameter> _parameters;
        private readonly StringBuilder _queryBuilder;
        private readonly List<ColumnAttribute> _columns;

        public ConnectionOptions<TDbConnection> ConnectionOptions => _connectionOptions;

        public BatchExecute(ConnectionOptions<TDbConnection> connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            _queries = new Queue<IQuery>();
            _parameters = new List<IDataParameter>();
            _queryBuilder = new();
            _columns = new();
        }

        public BatchExecute<TDbConnection> Add<T, TResult>(Func<ConnectionOptions<TDbConnection>, IQuery<T, TDbConnection, TResult>> expression) 
            where T : class, new()
        {
            IQuery query = expression.Invoke(_connectionOptions);
            _parameters.AddRange(query.GetParameters<T, TDbConnection>(_connectionOptions.DatabaseManagment));
            _queryBuilder.Append(query.Text);
            _columns.AddRange(query.Columns);
            _queries.Enqueue(query);
            return this;
        }

        public int Exec()
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagment.ExecuteNonQuery(query, _parameters);
        }

        public int Exec(TDbConnection connection)
        {
            var query = new BatchQuery(_queryBuilder.ToString(), _columns, null);
            return _connectionOptions.DatabaseManagment.ExecuteNonQuery(connection, query, _parameters);
        }
    }
}
