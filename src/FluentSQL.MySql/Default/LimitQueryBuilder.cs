using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.MySql.Default
{
    internal class LimitQueryBuilder<T> : QueryBuilderBase, IQueryBuilder<T, LimitQuery<T>> where T : class, new()
    {
        private readonly IQuery<T> _selectQuery;
        private readonly int _start;
        private readonly int? _length;

        public LimitQueryBuilder(IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, IStatements statements, int start, int? length)
            : base(ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements), ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
                statements, QueryType.Custom)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IAndOr<T, SelectQuery<T>> queryBuilder, IStatements statements, int start, int? length)
            : base(ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements), ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
                statements, QueryType.Custom)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IQueryBuilder<T, OrderByQuery<T>> queryBuilder, IStatements statements, int start, int? length)
            : base(ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements), ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
                statements, QueryType.Custom)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQuery<T> Build()
        {
            return new LimitQuery<T>(GenerateQuery(), _selectQuery.Columns, _selectQuery.Criteria, _selectQuery.Statements);
        }

        protected override string GenerateQuery()
        {
            string result = _selectQuery.Text.Replace(";", "");

            result = _length.HasValue ? $"{result} LIMIT {_start},{_length};" : $"{result} LIMIT {_start};";
            
            return result;
        }
    }

    internal class LimitQueryBuilder<T, TDbConnection> : QueryBuilderBase<TDbConnection>,
        IQueryBuilder<T, LimitQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>>,
        IBuilder<LimitQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IQuery<T, TDbConnection, IEnumerable<T>> _selectQuery;
        private readonly int _start;
        private readonly int? _length;

        public LimitQueryBuilder(IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions, int start, int? length)
            : base(connectionOptions != null ? ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(connectionOptions.Statements) : string.Empty,
                  ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions, connectionOptions!, QueryType.Custom)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IAndOr<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions, int start, int? length)
            : base(connectionOptions != null ? ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(connectionOptions.Statements) : string.Empty,
                  ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions, connectionOptions!, QueryType.Custom)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQueryBuilder(IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions, int start, int? length)
            : base(connectionOptions != null ? ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(connectionOptions.Statements) : string.Empty,
                  ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions, connectionOptions!, QueryType.Custom)
        {
            _selectQuery = queryBuilder.Build();
            _start = start;
            _length = length;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public LimitQuery<T,TDbConnection> Build()
        {
            return new LimitQuery<T,TDbConnection>(GenerateQuery(), _selectQuery.Columns, _selectQuery.Criteria, 
                new ConnectionOptions<TDbConnection>(_selectQuery.Statements, _selectQuery.DatabaseManagment));
        }

        protected override string GenerateQuery()
        {
            string result = _selectQuery.Text.Replace(";", "");

            result = _length.HasValue ? $"{result} LIMIT {_start},{_length};" : $"{result} LIMIT {_start};";

            return result;
        }
    }
}
