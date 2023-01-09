using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal class OrderByQueryBuilder<T> : QueryBuilderBase<T, OrderByQuery<T>>, IQueryBuilder<T, OrderByQuery<T>> where T : class, new()
    {
        private readonly IQueryBuilderWithWhere<T, SelectQuery<T>> _queryBuilder;
        private readonly IAndOr<T, SelectQuery<T>> _andorBuilder;
        private readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, IStatements statements)
            : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
           IAndOr<T, SelectQuery<T>> andOr, IStatements statements)
           : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        public override OrderByQuery<T> Build()
        {
            SelectQuery<T> selectQuery = _queryBuilder == null ? _andorBuilder.Build() : _queryBuilder.Build();
            var iswhere = selectQuery.Criteria != null && selectQuery.Criteria.Any();
            var query = CreateQuery(iswhere, _columnsByOrderBy, Statements, selectQuery.Columns, _tableName, 
                iswhere ? string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart)) : string.Empty);
            return new OrderByQuery<T>(query, selectQuery.Columns, selectQuery.Criteria, selectQuery.Statements);
        }

        internal static string CreateQuery(bool isWhere, Queue<ColumnsOrderBy> columnsByOrderBy, IStatements statements, IEnumerable<ColumnAttribute> columns, 
            string tableName, string criterias)
        {
            string columnsOrderby =
                string.Join(",", columnsByOrderBy.Select(x => $"{string.Join(",", x.Columns.Select(y => y.ColumnAttribute.GetColumnName(tableName, statements)))} {x.OrderBy}"));

            string result = string.Empty;

            if (!isWhere)
            {
                result = string.Format(statements.SelectOrderBy,
                     string.Join(",", columns.Select(x => x.GetColumnName(tableName, statements))),
                     tableName,
                     columnsOrderby);
            }
            else
            {
                result = string.Format(statements.SelectWhereOrderBy,
                     string.Join(",", columns.Select(x => x.GetColumnName(tableName, statements))),
                     tableName, criterias, columnsOrderby);
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
