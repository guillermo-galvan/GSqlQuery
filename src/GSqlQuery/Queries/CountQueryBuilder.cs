using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal class CountQueryBuilder<T> : QueryBuilderWithCriteria<T, CountQuery<T>>, IQueryBuilderWithWhere<T, CountQuery<T>> where T : class, new()
    {
        private readonly IQueryBuilder<T, SelectQuery<T>> _queryBuilder;

        public CountQueryBuilder(IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, IStatements statements)
            : base(statements)
        {
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
            _andOr = null;
        }

        public override CountQuery<T> Build()
        {
            SelectQuery<T> selectQuery = _queryBuilder.Build();
            var query = CreateQuery(_andOr != null, Statements, selectQuery.Columns, _tableName, _andOr != null ? GetCriteria() : string.Empty);
            return new CountQuery<T>(query, selectQuery.Columns, _criteria, _queryBuilder.Statements);
        }

        public override IWhere<T, CountQuery<T>> Where()
        {
            CountWhere<T> selectWhere = new CountWhere<T>(this);
            _andOr = selectWhere;
            return (IWhere<T, CountQuery<T>>)_andOr;
        }

        internal static string CreateQuery(bool isWhere, IStatements statements, IEnumerable<ColumnAttribute> columns, string tableName, string criterias)
        {
            string result = string.Empty;

            if (!isWhere)
            {
                result = string.Format(statements.Select,
                    $"COUNT({string.Join(",", columns.Select(x => x.GetColumnName(tableName, statements)))})",
                    tableName);
            }
            else
            {
                result = string.Format(statements.SelectWhere,
                    $"COUNT({string.Join(",", columns.Select(x => x.GetColumnName(tableName, statements)))})",
                    tableName, criterias);
            }

            return result;
        }
    }
}
