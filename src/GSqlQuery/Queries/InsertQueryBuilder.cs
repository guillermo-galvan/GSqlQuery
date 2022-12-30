using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal class InsertQueryBuilder<T> : QueryBuilderBase<T, InsertQuery<T>>, IQueryBuilder<T, InsertQuery<T>> where T : class, new()
    {
        private static ulong _idParam = 0;
        protected readonly object _entity;

        public InsertQueryBuilder(IStatements statements, object entity)
            : base(statements)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        internal static string CreateQuery(IStatements statements, IEnumerable<PropertyOptions> columns, string tableName, object entity, ref IEnumerable<CriteriaDetail> criteria)
        {
            AutoIncrementingClass autoIncrementingClass = GetValues(statements, columns, tableName, entity);
            CriteriaDetail criteriaDetail = new CriteriaDetail(string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail.Name)), autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail));
            criteria = new CriteriaDetail[] { criteriaDetail };
            string text = autoIncrementingClass.WithAutoIncrementing ?
                $"{string.Format(statements.Insert, tableName, string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName)), criteriaDetail.QueryPart)} {statements.ValueAutoIncrementingQuery}"
                : string.Format(statements.Insert, tableName, string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName)), criteriaDetail.QueryPart);

            return text;
        }

        internal static AutoIncrementingClass GetValues(IStatements statements, IEnumerable<PropertyOptions> columns, string tableName, object entity)
        {
            var columnsParameters = columns.Where(x => !x.ColumnAttribute.IsAutoIncrementing)
                          .Select(x => new ColumnParameterDetail(x.ColumnAttribute.GetColumnName(tableName, statements), new ParameterDetail($"@PI{_idParam++}", x.GetValue(entity), x)))
                          .ToArray();
            return new AutoIncrementingClass(columns.Any(x => x.ColumnAttribute.IsAutoIncrementing), columnsParameters);
        }

        public override InsertQuery<T> Build()
        {
            IEnumerable<CriteriaDetail> criteria = null;
            var query = CreateQuery(Statements, Columns, _tableName, _entity, ref criteria);
            return new InsertQuery<T>(query, Columns.Select(x => x.ColumnAttribute), criteria, Statements, _entity);
        }
    }
}
