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
    /// <param name="formats">Formats</param>
    /// <param name="entity">Entity</param>
    /// <exception cref="ArgumentNullException"></exception>
    internal abstract class InsertQueryBuilder<T, TReturn>(IFormats formats, object entity) : QueryBuilderBase<T, TReturn>(formats)
        where T : class
        where TReturn : InsertQuery<T>
    {
        protected readonly object _entity = entity ?? throw new ArgumentNullException(nameof(entity));

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="criteria">Criterias</param>
        /// <returns>Query text</returns>
        internal string CreateQuery(out IEnumerable<CriteriaDetail> criteria)
        {
            AutoIncrementingClass autoIncrementingClass = GetValues();

            string querypart = string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail.Name));
            IEnumerable<ParameterDetail> parameters = autoIncrementingClass.ColumnParameters.Select(x => x.ParameterDetail);

            CriteriaDetail criteriaDetail = new CriteriaDetail(querypart, parameters);
            criteria =[criteriaDetail];

            string columnNames = string.Join(",", autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName));

            string text = ConstFormat.INSERT.Replace("{0}", _tableName).Replace("{1}", columnNames).Replace("{2}", criteriaDetail.QueryPart);

            if (autoIncrementingClass.WithAutoIncrementing)
            {
                text += " " + Options.ValueAutoIncrementingQuery;
            }

            return text;
        }

        /// <summary>
        /// Get auto incremeting column
        /// </summary>
        /// <returns>AutoIncrementingClass</returns>
        internal AutoIncrementingClass GetValues()
        {
            ColumnParameterDetail[] columnsParameters = Columns.Where(x => !x.ColumnAttribute.IsAutoIncrementing)
                          .Select(x => new ColumnParameterDetail(Options.GetColumnName(_tableName, x.ColumnAttribute, QueryType.Create), new ParameterDetail($"@PI{Helpers.GetIdParam()}", x.GetValue(_entity), x)))
                          .ToArray();
            bool isAutoIncrement = Columns.Any(x => x.ColumnAttribute.IsAutoIncrementing);
            return new AutoIncrementingClass(isAutoIncrement, columnsParameters);
        }
    }

    /// <summary>
    /// Insert Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <param name="formats">Formats</param>
    /// <param name="entity">Entity</param>
    internal class InsertQueryBuilder<T>(IFormats formats, object entity) : InsertQueryBuilder<T, InsertQuery<T>>(formats, entity)
        where T : class
    {

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Insert Query</returns>
        public override InsertQuery<T> Build()
        {
            string query = CreateQuery(out IEnumerable<CriteriaDetail> criteria);
            return new InsertQuery<T>(query, Columns, criteria, Options);
        }
    }
}