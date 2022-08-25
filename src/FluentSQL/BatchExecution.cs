using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using System.Data;
using System.Data.Common;
using System.Text;

namespace FluentSQL
{
    public class BatchExecute
    {
        private readonly ConnectionOptions _connectionOptions;
        private readonly Queue<IQuery> _queries;
        private readonly List<IDataParameter> _parameters;
        private readonly StringBuilder _queryBuilder;
        private readonly List<ColumnAttribute> _columns;

        public BatchExecute(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            connectionOptions.DatabaseManagment.ValidateDatabaseManagment();
            _queries = new Queue<IQuery>();
            _parameters = new List<IDataParameter>();
            _queryBuilder = new();
            _columns = new();
        }

        public BatchExecute Add<T>(Func<ConnectionOptions, IQuery<T>> expression) where T : class, new()
        {
            IQuery<T> query = expression.Invoke(_connectionOptions);
            _parameters.AddRange(query.GetParameters());
            _queryBuilder.Append(query.Text);
            _columns.AddRange(query.Columns);
            _queries.Enqueue(query);
            return this;
        }

        public int? Exec()
        {
            return (int?)new BatchQuery(_queryBuilder.ToString(), _columns, null, _connectionOptions, _parameters).Exec();
        }

        public int? ExecWithTransaction()
        {
            int? result = null;

            using DbConnection connection = _connectionOptions.DatabaseManagment.GetConnection();

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            using DbTransaction transaction = connection.BeginTransaction();

            if (transaction != null && transaction.Connection != null)
            {
                result = Exec(transaction.Connection);
                transaction.Commit();
            }

            connection.Close();
            return result;
        }

        public int? Exec(DbConnection connection)
        {
            return (int?)new BatchQuery(_queryBuilder.ToString(), _columns, null, _connectionOptions, _parameters).Exec(connection);
        }
    }
}
