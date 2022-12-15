using GSqlQuery.Extensions;

namespace GSqlQuery.Runner.Queries
{
    internal class OrderByQueryBuilder<T, TDbConnection> : QueryBuilderBase<T, OrderByQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, OrderByQuery<T, TDbConnection>, TDbConnection>,
        IBuilder<OrderByQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection>? _queryBuilder;
        private SelectQuery<T, TDbConnection>? _selectQuery;
        private readonly IAndOr<T, SelectQuery<T, TDbConnection>>? _andorBuilder;
        private readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions, QueryType.Custom)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IAndOr<T, SelectQuery<T, TDbConnection>> andOr,
            ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions, QueryType.Custom)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public override OrderByQuery<T, TDbConnection> Build()
        {
            _selectQuery = _queryBuilder == null ? _andorBuilder!.Build() : _queryBuilder.Build();
            return new OrderByQuery<T, TDbConnection>(GenerateQuery(), _selectQuery.Columns, _selectQuery.Criteria, ConnectionOptions);
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
