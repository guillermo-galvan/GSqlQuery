using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    public class ColumnParameterDetail
    {
        public string ColumnName { get; set; }

        public ParameterDetail ParameterDetail { get; set; }

        public ColumnParameterDetail(string columnName, ParameterDetail parameterDetail)
        {
            ColumnName = columnName;
            ParameterDetail = parameterDetail;
        }
    }

    internal class InsertQueryBuilder<T> : QueryBuilderBase<T, InsertQuery<T>>, IQueryBuilder<T, InsertQuery<T>> where T : class, new()
    {
        protected readonly object _entity;
        protected IEnumerable<CriteriaDetail> _criteria = null;
        protected PropertyOptions _propertyOptionsAutoIncrementing = null;

        public InsertQueryBuilder(IStatements statements, object entity)
            : base(statements, QueryType.Insert)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        protected string GetInsertQuery()
        {
            ColumnParameterDetail[] values = GetValues();
            CriteriaDetail criteriaDetail = new CriteriaDetail(string.Join(",", values.Select(x => x.ParameterDetail.Name)), values.Select(x => x.ParameterDetail));
            _criteria = new CriteriaDetail[] { criteriaDetail };
            string text = _propertyOptionsAutoIncrementing != null ?
                $"{string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.ColumnName)), criteriaDetail.QueryPart)} {Statements.ValueAutoIncrementingQuery}"
                : string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.ColumnName)), criteriaDetail.QueryPart);

            return text;
        }

        protected ColumnParameterDetail[] GetValues()
        {
            long ticks = DateTime.Now.Ticks;
            _propertyOptionsAutoIncrementing = Columns.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing);
            return Columns.Where(x => !x.ColumnAttribute.IsAutoIncrementing)
                          .Select(x => new  ColumnParameterDetail(x.ColumnAttribute.GetColumnName(_tableName, Statements), new ParameterDetail($"@PI{ticks++}", x.GetValue(_entity), x)))
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
