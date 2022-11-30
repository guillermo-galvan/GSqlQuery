using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.DatabaseManagement.Default
{
    internal class InsertQueryBuilder<T, TDbConnection> : QueryBuilderBase<T, InsertQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection>,
        IBuilder<InsertQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly object _entity;
        protected bool _includeAutoIncrementing;
        protected IEnumerable<CriteriaDetail>? _criteria = null;

        public InsertQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity)
            : base(connectionOptions, QueryType.Insert)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        protected string GetInsertQuery()
        {
            Queue<(string columnName, ParameterDetail parameterDetail)> values = GetValues();
            CriteriaDetail criteriaDetail = new(string.Join(",", values.Select(x => x.parameterDetail.Name)), values.Select(x => x.parameterDetail));
            _criteria = new CriteriaDetail[] { criteriaDetail };
            string text = _includeAutoIncrementing ?
                $"{string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart)} {Statements.ValueAutoIncrementingQuery}"
                : string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart);

            return text;
        }

        protected (string columnName, ParameterDetail parameterDetail) GetParameterValue(ColumnAttribute column)
        {
            PropertyOptions options = Columns.First(x => x.ColumnAttribute.Name == column.Name);
            return (column.GetColumnName(_tableName, Statements), new ParameterDetail($"@PI{DateTime.Now.Ticks}", options.GetValue(_entity), options));
        }

        protected Queue<(string columnName, ParameterDetail parameterDetail)> GetValues()
        {
            Queue<(string columnName, ParameterDetail parameterDetail)> values = new();
            _includeAutoIncrementing = false;
            foreach (PropertyOptions item in Columns)
            {
                if (!item.ColumnAttribute.IsAutoIncrementing)
                {
                    values.Enqueue(GetParameterValue(item.ColumnAttribute));
                }
                else
                {
                    _includeAutoIncrementing = true;
                }
            }
            return values;
        }

        protected override string GenerateQuery()
        {
            return GetInsertQuery();
        }

        public override InsertQuery<T, TDbConnection> Build()
        {
            return new InsertQuery<T, TDbConnection>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions, _entity);
        }
    }
}
