﻿using GSqlQuery.Extensions;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal class CountQueryBuilder<T> : QueryBuilderWithCriteria<T, CountQuery<T>>, IQueryBuilderWithWhere<T, CountQuery<T>> where T : class, new()
    {
        private readonly IQueryBuilder<T, SelectQuery<T>> _queryBuilder;
        private SelectQuery<T> _selectQuery;

        public CountQueryBuilder(IQueryBuilderWithWhere<T, SelectQuery<T>> queryBuilder, IStatements statements)
            : base(statements, QueryType.Custom)
        {
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
            _andOr = null;
        }

        public override CountQuery<T> Build()
        {
            _selectQuery = _queryBuilder.Build();
            return new CountQuery<T>(GenerateQuery(), _selectQuery.Columns, _criteria, _queryBuilder.Statements);
        }

        public override IWhere<T, CountQuery<T>> Where()
        {
            CountWhere<T> selectWhere = new CountWhere<T>(this);
            _andOr = selectWhere;
            return (IWhere<T, CountQuery<T>>)_andOr;
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_andOr == null)
            {
                result = string.Format(Statements.Select,
                    $"COUNT({string.Join(",", _selectQuery.Columns.Select(x => x.GetColumnName(_tableName, Statements)))})",
                    _tableName);
            }
            else
            {
                result = string.Format(Statements.SelectWhere,
                    $"COUNT({string.Join(",", _selectQuery.Columns.Select(x => x.GetColumnName(_tableName, Statements)))})",
                    _tableName, GetCriteria());
            }

            return result;
        }
    }
}