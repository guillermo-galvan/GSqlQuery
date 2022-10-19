using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    internal class OrderByQueryBuilder<T> : QueryBuilderBase, IQueryBuilder<T, OrderByQuery<T>> where T : class, new()
    {
        private readonly IQueryBuilderWithWhere<T, SelectQuery<T>>? _queryBuilder;
        private readonly IAndOr<T, SelectQuery<T>>? _andorBuilder;
        private SelectQuery<T>? _selectQuery;
        private readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, IStatements statements)
            : base(ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements), ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
                statements, QueryType.Custom)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new ();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
           IAndOr<T, SelectQuery<T>> andOr, IStatements statements)
           : base(ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements), ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
               statements, QueryType.Custom)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public OrderByQuery<T> Build()
        {
            _selectQuery = _queryBuilder == null ? _andorBuilder!.Build() : _queryBuilder.Build();
            return new OrderByQuery<T>(GenerateQuery(), _selectQuery.Columns, _selectQuery.Criteria, _selectQuery.Statements);
        }

        protected override string GenerateQuery()
        {
            string columnsOrderby = 
                string.Join(",", _columnsByOrderBy.Select(x => $"{string.Join(",", x.Columns.Select(y => y.ColumnAttribute.GetColumnName(_tableName, Statements)))} {x.OrderBy}"));
            string result = string.Empty;
           
            if (_selectQuery!.Criteria == null || !_selectQuery.Criteria.Any())
            {
                result = string.Format(Statements.SelectOrderBy,
                     string.Join(",", _selectQuery.Columns.Select(x => x.GetColumnName(_tableName, Statements))),
                     _tableName,
                     columnsOrderby);
            }
            else
            {
                result = string.Format(Statements.SelectWhereOrderBy,
                     string.Join(",", _selectQuery.Columns.Select(x => x.GetColumnName(_tableName, Statements))),
                     _tableName, string.Join(" ", _selectQuery.Criteria.Select(x => x.QueryPart)), columnsOrderby);
            }

            return result;
        }

        internal void AddOrderBy(IEnumerable<string> selectMember, OrderBy orderBy)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
        }
    }

    internal class OrderByQueryBuilder<T, TDbConnection> : QueryBuilderBase<TDbConnection>,
        IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>>,
        IBuilder<OrderByQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>>? _queryBuilder;
        private SelectQuery<T, TDbConnection>? _selectQuery;
        private readonly IAndOr<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>>? _andorBuilder;
        private readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions != null ? ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(connectionOptions.Statements) : string.Empty,
                  ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions, connectionOptions!, QueryType.Custom)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IAndOr<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> andOr,
            ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions != null ? ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(connectionOptions.Statements) : string.Empty,
                  ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions, connectionOptions!, QueryType.Custom)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public OrderByQuery<T, TDbConnection> Build()
        {
            _selectQuery = _queryBuilder == null ? _andorBuilder!.Build() : _queryBuilder.Build();
            return new OrderByQuery<T,TDbConnection>(GenerateQuery(), _selectQuery.Columns, _selectQuery.Criteria, ConnectionOptions);
        }

        protected override string GenerateQuery()
        {
            string columnsOrderby =
                string.Join(",", _columnsByOrderBy.Select(x => 
                $"{string.Join(",", x.Columns.Select(y => y.ColumnAttribute.GetColumnName(_tableName, ConnectionOptions.Statements)))} {x.OrderBy}"));
            string result = string.Empty;

            if (_selectQuery!.Criteria == null || !_selectQuery.Criteria.Any())
            {
                result = string.Format(ConnectionOptions.Statements.SelectOrderBy,
                     string.Join(",", _selectQuery.Columns.Select(x => x.GetColumnName(_tableName, ConnectionOptions.Statements))),
                     _tableName,
                     columnsOrderby);
            }
            else
            {
                result = string.Format(ConnectionOptions.Statements.SelectWhereOrderBy,
                     string.Join(",", _selectQuery.Columns.Select(x => x.GetColumnName(_tableName, ConnectionOptions.Statements))),
                     _tableName, string.Join(" ", _selectQuery.Criteria.Select(x => x.QueryPart)), columnsOrderby);
            }

            return result;
        }

        internal void AddOrderBy(IEnumerable<string> selectMember, OrderBy orderBy)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
        }
    }
}
