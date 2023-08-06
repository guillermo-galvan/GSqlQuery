using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal interface IOrderByQueryBuilder
    {
        void AddOrderBy(IEnumerable<string> selectMember, OrderBy orderBy);
    }

    internal abstract class OrderByQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn>, IOrderByQueryBuilder
        where T : class, new()
        where TReturn : OrderByQuery<T>
        where TSelectQuery : SelectQuery<T>
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery> _andorBuilder;
        protected readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        protected OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, TSelectQuery, TOptions> queryBuilder, IStatements statements) : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        protected OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
           IAndOr<T, TSelectQuery> andOr, IStatements statements) : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public void AddOrderBy(IEnumerable<string> selectMember, OrderBy orderBy)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
        }

        internal string CreateQuery(IStatements statements, out IEnumerable<ColumnAttribute> columns, out IEnumerable<CriteriaDetail> criteria)
        {
            TSelectQuery selectQuery = _queryBuilder == null ? _andorBuilder.Build() : _queryBuilder.Build();
            string columnsOrderby =
                string.Join(",", _columnsByOrderBy.Select(x => $"{string.Join(",", x.Columns.Select(y => y.ColumnAttribute.GetColumnName(_tableName, statements, QueryType.Read)))} {x.OrderBy}"));

            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            string result = string.Empty;

            if (selectQuery.Criteria == null || !selectQuery.Criteria.Any())
            {
                result = string.Format(statements.SelectOrderBy,
                     string.Join(",", columns.Select(x => x.GetColumnName(_tableName, statements, QueryType.Read))),
                     _tableName,
                     columnsOrderby);
            }
            else
            {
                result = string.Format(statements.SelectWhereOrderBy,
                     string.Join(",", columns.Select(x => x.GetColumnName(_tableName, statements, QueryType.Read))),
                     _tableName, string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart)), columnsOrderby);
            }

            return result;
        }
    }

    internal class OrderByQueryBuilder<T> : OrderByQueryBuilder<T, OrderByQuery<T>, IStatements, SelectQuery<T>>,
        IQueryBuilder<OrderByQuery<T>, IStatements>
        where T : class, new()
    {
        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T>, IStatements> queryBuilder)
            : base(selectMember, orderBy, queryBuilder, queryBuilder.Options)
        { }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
           IAndOr<T, SelectQuery<T>> andOr, IStatements statements)
           : base(selectMember, orderBy, andOr, statements)
        { }

        public override OrderByQuery<T> Build()
        {
            var query = CreateQuery(Options, out IEnumerable<ColumnAttribute> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T>(query, columns, criteria, Options);
        }
    }
}