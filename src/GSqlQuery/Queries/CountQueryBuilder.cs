using GSqlQuery.Extensions;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal abstract class CountQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn>
        where T : class
        where TReturn : CountQuery<T>
        where TSelectQuery : SelectQuery<T>
    {
        protected readonly IQueryBuilder<TSelectQuery, TOptions> _queryBuilder;

        public CountQueryBuilder(IQueryBuilderWithWhere<TSelectQuery, TOptions> queryBuilder, IStatements statements) : base(statements)
        {
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        internal string CreateQuery(IStatements statements)
        {
            string result = string.Empty;
            var selectQuery = _queryBuilder.Build();
            Columns = _queryBuilder.Columns;

            if (_andOr == null)
            {
                result = string.Format(statements.Select,
                    $"COUNT({string.Join(",", selectQuery.Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, statements, QueryType.Read)))})",
                    _tableName);
            }
            else
            {
                result = string.Format(statements.SelectWhere,
                    $"COUNT({string.Join(",", selectQuery.Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, statements, QueryType.Read)))})",
                    _tableName, GetCriteria());
            }

            return result;
        }
    }

    internal class CountQueryBuilder<T> : CountQueryBuilder<T, CountQuery<T>, IStatements, SelectQuery<T>> where T : class
    {
        public CountQueryBuilder(IQueryBuilderWithWhere<SelectQuery<T>, IStatements> queryBuilder)
            : base(queryBuilder, queryBuilder.Options)
        { }

        public override CountQuery<T> Build()
        {
            var query = CreateQuery(Options);
            return new CountQuery<T>(query, Columns, _criteria, _queryBuilder.Options);
        }
    }
}