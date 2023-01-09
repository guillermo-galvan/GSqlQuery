using GSqlQuery.Runner;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.MySql
{
    internal class LimitQueryBuilder<T> : QueryBuilderBase<T, LimitQuery<T>>, IQueryBuilder<T, LimitQuery<T>> where T : class, new()
    {
        private readonly IQuery<T> _selectQuery;
        private readonly int _start;
        private readonly int? _length;

        public LimitQueryBuilder(IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, IStatements statements, int start, int? length)
            : base(statements)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IAndOr<T, SelectQuery<T>> queryBuilder, IStatements statements, int start, int? length)
            : base(statements)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IQueryBuilder<T, OrderByQuery<T>> queryBuilder, IStatements statements, int start, int? length)
            : base(statements)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public override LimitQuery<T> Build()
        {
            var query = GenerateQuery(_selectQuery, _start, _length);
            return new LimitQuery<T>(query, _selectQuery.Columns, _selectQuery.Criteria, _selectQuery.Statements);
        }

        internal static string GenerateQuery(IQuery selectQuery, int start, int? length)
        {
            string result = selectQuery.Text.Replace(";", "");
            result = length.HasValue ? $"{result} LIMIT {start},{length};" : $"{result} LIMIT {start};";
            return result;
        }
    }

    internal class LimitQueryBuilder<T, TDbConnection> : QueryBuilderBase<T, LimitQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, LimitQuery<T, TDbConnection>, TDbConnection>,
        IBuilder<LimitQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IQuery<T, TDbConnection, IEnumerable<T>> _selectQuery;
        private readonly int _start;
        private readonly int? _length;

        public LimitQueryBuilder(IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions, int start, int? length)
            : base(connectionOptions)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IAndOr<T, SelectQuery<T, TDbConnection>> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions, int start, int? length)
            : base(connectionOptions)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions, int start, int? length)
            : base(connectionOptions)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public override LimitQuery<T, TDbConnection> Build()
        {
            var query = LimitQueryBuilder<T>.GenerateQuery(_selectQuery, _start, _length);
            return new LimitQuery<T, TDbConnection>(query, _selectQuery.Columns, _selectQuery.Criteria,
                new ConnectionOptions<TDbConnection>(_selectQuery.Statements, _selectQuery.DatabaseManagement));
        }
    }
}
