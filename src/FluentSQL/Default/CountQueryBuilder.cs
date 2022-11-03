using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    internal class CountQueryBuilder<T> : QueryBuilderWithCriteria<T, CountQuery<T>>, IQueryBuilderWithWhere<T, CountQuery<T>> where T : class, new()
    {
        private readonly IQueryBuilderWithWhere<T, SelectQuery<T>> _queryBuilder;
        private SelectQuery<T>? _selectQuery;

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
            CountWhere<T> selectWhere = new(this);
            _andOr = selectWhere;
            return (IWhere<T, CountQuery<T>>)_andOr;
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_andOr == null)
            {
                result = string.Format(Statements.Select,
                    $"COUNT({string.Join(",", _selectQuery!.Columns.Select(x => x.GetColumnName(_tableName, Statements)))})",
                    _tableName);
            }
            else
            {
                result = string.Format(Statements.SelectWhere,
                    $"COUNT({string.Join(",", _selectQuery!.Columns.Select(x => x.GetColumnName(_tableName, Statements)))})",
                    _tableName, GetCriteria());
            }

            return result;
        }
    }

    internal class CountQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, CountQuery<T, TDbConnection>, TDbConnection, int>,
        IQueryBuilderWithWhere<T, CountQuery<T, TDbConnection>, TDbConnection, int>,
        IQueryBuilder<T, CountQuery<T, TDbConnection>, TDbConnection, int>, IBuilder<CountQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>> _queryBuilder;
        private SelectQuery<T, TDbConnection>? _selectQuery;

        public CountQueryBuilder(IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, TDbConnection, IEnumerable<T>>  queryBuilder ,
            ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions, QueryType.Custom)
        {
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
            _andOr = null;
        }

        public override CountQuery<T, TDbConnection> Build()
        {
            _selectQuery = _queryBuilder.Build();
            return new CountQuery<T,TDbConnection>(GenerateQuery(), _selectQuery.Columns, _criteria, _queryBuilder.ConnectionOptions);
        }

        public override IWhere<T, CountQuery<T, TDbConnection>> Where()
        {
            CountWhere<T, TDbConnection> selectWhere = new(this);
            _andOr = selectWhere;
            return (IWhere<T, CountQuery<T, TDbConnection>>)_andOr;
        }

        protected override string GenerateQuery()
        {
            string result = string.Empty;

            if (_andOr == null)
            {
                result = string.Format(_queryBuilder.ConnectionOptions.Statements.Select,
                    $"COUNT({string.Join(",", _selectQuery!.Columns.Select(x => x.GetColumnName(_tableName, _queryBuilder.ConnectionOptions.Statements)))})",
                    _tableName);
            }
            else
            {
                result = string.Format(_queryBuilder.ConnectionOptions.Statements.SelectWhere,
                    $"COUNT({string.Join(",", _selectQuery!.Columns.Select(x => x.GetColumnName(_tableName, _queryBuilder.ConnectionOptions.Statements)))})",
                    _tableName, GetCriteria());
            }

            return result;
        }
    }
}
