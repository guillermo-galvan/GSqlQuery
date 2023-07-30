using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal abstract class InsertQueryBuilder<T, TReturn> : QueryBuilderBase<T, TReturn>
        where T : class, new()
        where TReturn : InsertQuery<T>
    {
        private static ulong _idParam = 0;
        protected readonly object _entity;

        public InsertQueryBuilder(IStatements statements, object entity)
             : base(statements)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            if (_idParam > ulong.MaxValue - 2100)
            {
                _idParam = 0;
            }
        }

        internal string CreateQuery(IStatements statements, out IEnumerable<CriteriaDetail> criteria)
        {
            AutoIncrementingClass autoIncrementingClass = GetValues(statements);
            CriteriaDetail criteriaDetail = new CriteriaDetail(string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail.Name)), autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail));
            criteria = new CriteriaDetail[] { criteriaDetail };
            string text = autoIncrementingClass.WithAutoIncrementing ?
                $"{string.Format(statements.Insert, _tableName, string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName)), criteriaDetail.QueryPart)} {statements.ValueAutoIncrementingQuery}"
                : string.Format(statements.Insert, _tableName, string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName)), criteriaDetail.QueryPart);

            return text;
        }

        internal AutoIncrementingClass GetValues(IStatements statements)
        {
            var columnsParameters = Columns.Where(x => !x.ColumnAttribute.IsAutoIncrementing)
                          .Select(x => new ColumnParameterDetail(x.ColumnAttribute.GetColumnName(_tableName, statements), new ParameterDetail($"@PI{_idParam++}", x.GetValue(_entity), x)))
                          .ToArray();
            return new AutoIncrementingClass(Columns.Any(x => x.ColumnAttribute.IsAutoIncrementing), columnsParameters);
        }
    }

    internal class InsertQueryBuilder<T> : InsertQueryBuilder<T, InsertQuery<T>>
        where T : class, new()
    {
        public InsertQueryBuilder(IStatements statements, object entity)
             : base(statements, entity)
        {

        }

        public override InsertQuery<T> Build()
        {
            var query = CreateQuery(Options, out IEnumerable<CriteriaDetail> criteria);
            return new InsertQuery<T>(query, Columns.Select(x => x.ColumnAttribute), criteria, Options, _entity);
        }
    }
}