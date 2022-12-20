using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Runner.Queries
{
    internal class InsertQueryBuilder<T, TDbConnection> : QueryBuilderBase<T, InsertQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, InsertQuery<T, TDbConnection>, TDbConnection>,
        IBuilder<InsertQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly object _entity;
        protected IEnumerable<CriteriaDetail> _criteria = null;
        protected PropertyOptions _propertyOptionsAutoIncrementing;

        public InsertQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object entity)
            : base(connectionOptions, QueryType.Insert)
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
                          .Select(x => new ColumnParameterDetail(x.ColumnAttribute.GetColumnName(_tableName, Statements), new ParameterDetail($"@PI{ticks++}", x.GetValue(_entity), x)))
                          .ToArray();
        }

        protected override string GenerateQuery()
        {
            return GetInsertQuery();
        }

        public override InsertQuery<T, TDbConnection> Build()
        {
            return new InsertQuery<T, TDbConnection>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions, _entity, _propertyOptionsAutoIncrementing);
        }
    }
}
