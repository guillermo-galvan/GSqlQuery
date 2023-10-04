using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Insert Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    internal abstract class InsertQueryBuilder<T, TReturn> : QueryBuilderBase<T, TReturn>
        where T : class
        where TReturn : InsertQuery<T>
    {
        protected readonly object _entity;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public InsertQueryBuilder(IFormats formats, object entity)
             : base(formats)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="criteria">Criterias</param>
        /// <returns>Query text</returns>
        internal string CreateQuery(out IEnumerable<CriteriaDetail> criteria)
        {
            AutoIncrementingClass autoIncrementingClass = GetValues();
            CriteriaDetail criteriaDetail = new CriteriaDetail(string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail.Name)), autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail));
            criteria = new CriteriaDetail[] { criteriaDetail };
            string text = autoIncrementingClass.WithAutoIncrementing ?
                $"{string.Format(ConstFormat.INSERT, _tableName, string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName)), criteriaDetail.QueryPart)} {Options.ValueAutoIncrementingQuery}"
                : string.Format(ConstFormat.INSERT, _tableName, string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName)), criteriaDetail.QueryPart);

            return text;
        }

        /// <summary>
        /// Get auto incremeting column
        /// </summary>
        /// <returns>AutoIncrementingClass</returns>
        internal AutoIncrementingClass GetValues()
        {
            var columnsParameters = Columns.Where(x => !x.ColumnAttribute.IsAutoIncrementing)
                          .Select(x => new ColumnParameterDetail(x.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Create), new ParameterDetail($"@PI{Helpers.GetIdParam()}", x.GetValue(_entity), x)))
                          .ToArray();
            return new AutoIncrementingClass(Columns.Any(x => x.ColumnAttribute.IsAutoIncrementing), columnsParameters);
        }
    }

    /// <summary>
    /// Insert Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class InsertQueryBuilder<T> : InsertQueryBuilder<T, InsertQuery<T>>
        where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <param name="entity">Entity</param>
        public InsertQueryBuilder(IFormats formats, object entity)
             : base(formats, entity)
        {

        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Insert Query</returns>
        public override InsertQuery<T> Build()
        {
            var query = CreateQuery(out IEnumerable<CriteriaDetail> criteria);
            return new InsertQuery<T>(query, Columns, criteria, Options);
        }
    }
}