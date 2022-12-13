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
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected PropertyOptions? _propertyOptionsAutoIncrementing;

        public InsertQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity)
            : base(connectionOptions, QueryType.Insert)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        protected string GetInsertQuery()
        {
            (string columnName, ParameterDetail parameterDetail)[] values = GetValues();
            CriteriaDetail criteriaDetail = new(string.Join(",", values.Select(x => x.parameterDetail.Name)), values.Select(x => x.parameterDetail));
            _criteria = new CriteriaDetail[] { criteriaDetail };
            string text = _propertyOptionsAutoIncrementing != null ?
                $"{string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart)} {Statements.ValueAutoIncrementingQuery}"
                : string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart);

            return text;
        }

        protected (string columnName, ParameterDetail parameterDetail)[] GetValues()
        {
            long ticks = DateTime.Now.Ticks;
            _propertyOptionsAutoIncrementing = Columns.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing);
            return Columns.Where(x => !x.ColumnAttribute.IsAutoIncrementing)
                          .Select(x => (x.ColumnAttribute.GetColumnName(_tableName, Statements), new ParameterDetail($"@PI{ticks++}", x.GetValue(_entity), x)))
                          .ToArray();
        }

        protected override string GenerateQuery()
        {
            return GetInsertQuery();
        }

        public override InsertQuery<T, TDbConnection> Build()
        {
            return new InsertQuery<T, TDbConnection>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions, _entity,_propertyOptionsAutoIncrementing);
        }
    }
}
