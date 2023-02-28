using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class OrderByQueryBuilder<T, TDbConnection> : QueryBuilderBase<T, OrderByQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderRunner<T, OrderByQuery<T, TDbConnection>, TDbConnection>,
        IBuilder<OrderByQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IQueryBuilderWithWhereRunner<T, SelectQuery<T, TDbConnection>, TDbConnection> _queryBuilder;
        private readonly IAndOr<T, SelectQuery<T, TDbConnection>> _andorBuilder;
        private readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhereRunner<T, SelectQuery<T, TDbConnection>, TDbConnection> queryBuilder,
            ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IAndOr<T, SelectQuery<T, TDbConnection>> andOr,
            ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public override OrderByQuery<T, TDbConnection> Build()
        {
            SelectQuery<T, TDbConnection> selectQuery = _queryBuilder == null ? _andorBuilder.Build() : _queryBuilder.Build();
            var iswhere = selectQuery.Criteria != null && selectQuery.Criteria.Any();
            var query = GSqlQuery.Queries.OrderByQueryBuilder<T>.CreateQuery(iswhere, _columnsByOrderBy, Options.Statements, selectQuery.Columns, _tableName, 
                iswhere ? string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart)) : string.Empty);
            return new OrderByQuery<T, TDbConnection>(query, selectQuery.Columns, selectQuery.Criteria, Options);
        }

        internal void AddOrderBy(IEnumerable<string> selectMember, OrderBy orderBy)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
        }
    }
}
