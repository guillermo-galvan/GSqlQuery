using FluentSQL.Extensions;
using System.Data;
using System.Text;

namespace FluentSQL
{
    public class BatchExecute<TDbConnection>
    {
        private readonly IStatements _statements;
        private readonly Queue<IQuery> _queries;
        private readonly List<IDataParameter> _parameters;
        private readonly StringBuilder _queryBuilder;
        private readonly List<ColumnAttribute> _columns;
        private readonly IDatabaseManagement<TDbConnection> _databaseManagment;

        public IDatabaseManagement<TDbConnection> DatabaseManagement => _databaseManagment;

        public BatchExecute(IStatements statements, IDatabaseManagement<TDbConnection> databaseManagment)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
            _databaseManagment = databaseManagment ?? throw new ArgumentNullException(nameof(databaseManagment));
            _queries = new Queue<IQuery>();
            _parameters = new List<IDataParameter>();
            _queryBuilder = new();
            _columns = new();
        }

        public BatchExecute<TDbConnection> Add<T>(Func<IStatements, IQuery<T>> expression) where T : class, new()
        {
            IQuery<T> query = expression.Invoke(_statements);
            _parameters.AddRange(query.GetParameters<T, TDbConnection>(_databaseManagment));
            _queryBuilder.Append(query.Text);
            _columns.AddRange(query.Columns);
            _queries.Enqueue(query);
            return this;
        }

        public int Exec()
        {
            //return new BatchQuery(_queryBuilder.ToString(), _columns, null, _statements, _parameters).SetDatabaseManagement(_databaseManagment).Exec();
            throw new NotImplementedException();

        }

        public int Exec(TDbConnection connection)
        {
            //return new BatchQuery(_queryBuilder.ToString(), _columns, null, _statements, _parameters).SetDatabaseManagement(_databaseManagment)
            //    .Exec(connection);
            throw new NotImplementedException();
        }
    }
}
