using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using System.Data;
using System.Text;

namespace FluentSQL
{
    public class BatchExecute<TDbConnection>: IExecute<int,TDbConnection>
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;
        private readonly Queue<IQuery> _queries;
        private readonly Queue<IDataParameter> _parameters;
        private readonly StringBuilder _queryBuilder;
        private readonly Queue<ColumnAttribute> _columns;

        public ConnectionOptions<TDbConnection> ConnectionOptions => _connectionOptions;

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
