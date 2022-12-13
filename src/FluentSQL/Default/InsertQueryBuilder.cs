using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    internal class InsertQueryBuilder<T> : QueryBuilderBase<T, InsertQuery<T>>, IQueryBuilder<T, InsertQuery<T>> where T : class, new()
    {
        protected readonly object _entity;
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected PropertyOptions? _propertyOptionsAutoIncrementing = null;
 
        public InsertQueryBuilder(IStatements statements, object entity)
            : base(statements, QueryType.Insert)
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

        public override InsertQuery<T> Build()
        {
            return new InsertQuery<T>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, Statements, _entity);
        }
       
        protected override string GenerateQuery()
        {
            return GetInsertQuery();
        }
    }
}
