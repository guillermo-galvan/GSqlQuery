using GSqlQuery.Cache;
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
    internal abstract class InsertQueryBuilder<T, TReturn, TQueryOptions>(TQueryOptions queryOptions, object entity) : QueryBuilderBase<T, TReturn, TQueryOptions>(queryOptions)
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected readonly object _entity = entity ?? throw new ArgumentNullException(nameof(entity));

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="criteria">Criterias</param>
        /// <returns>Query text</returns>
        internal string CreateQueryText(out IEnumerable<CriteriaDetailCollection> criteria)
        {
            AutoIncrementingClass autoIncrementingClass = GetValues();

            IEnumerable<ParameterDetail> parameters = autoIncrementingClass.ColumnParameters.SelectMany(x => x.CriteriaDetail.Values);
            string querypart = string.Join(",", parameters.Select(x => x.Name));

            criteria = autoIncrementingClass.ColumnParameters.Select(x => x.CriteriaDetail);
            IEnumerable<string> columnsName = autoIncrementingClass.ColumnParameters.Select(x => x.ColumnName);
            string columnNames = string.Join(",", columnsName);

            string text = ConstFormat.INSERT.Replace("{0}", _tableName).Replace("{1}", columnNames).Replace("{2}", querypart);

            if (autoIncrementingClass.WithAutoIncrementing)
            {
                return text + " " + QueryOptions.Formats.ValueAutoIncrementingQuery;
            }

            return text;
        }

        /// <summary>
        /// Get auto incremeting column
        /// </summary>
        /// <returns>AutoIncrementingClass</returns>
        internal AutoIncrementingClass GetValues()
        {
            IEnumerable<PropertyOptions> propertyOptions = Columns.Values.Where(x => !x.ColumnAttribute.IsAutoIncrementing);
            Queue<ColumnParameterDetail> tmpColumnsParameters = new Queue<ColumnParameterDetail>();
            int count = 0;

            foreach (PropertyOptions x in propertyOptions)
            {
                string columnName = x.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Create);
                object value = ExpressionExtension.GetValue(x, _entity);
                string parameterName = "@PI" + count++;
                ParameterDetail parameterDetail = new ParameterDetail(parameterName, value);
                ColumnParameterDetail columnParameterDetail = new ColumnParameterDetail(columnName, new CriteriaDetailCollection(parameterName, x, [parameterDetail]));
                tmpColumnsParameters.Enqueue(columnParameterDetail);
            }

            ColumnParameterDetail[] columnsParameters = [.. tmpColumnsParameters];

            bool isAutoIncrement = Columns.Count != columnsParameters.Length;
            return new AutoIncrementingClass(isAutoIncrement, columnsParameters);
        }

        public override TReturn Build()
        {
            return CacheQueryBuilderExtension.CreateInsertQuery<T, TReturn, TQueryOptions>(QueryOptions, entity, CreateQuery, GetQuery);
        }

        public TReturn CreateQuery()
        {
            string text = CreateQueryText(out IEnumerable<CriteriaDetailCollection> criteria);
            return GetQuery(text, Columns, criteria, QueryOptions);
        }

        public abstract TReturn GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions);
    }

    /// <summary>
    /// Insert Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <param name="formats">Formats</param>
    /// <param name="entity">Entity</param>
    internal class InsertQueryBuilder<T>(QueryOptions queryOptions, object entity) : InsertQueryBuilder<T, InsertQuery<T>, QueryOptions>(queryOptions, entity)
        where T : class
    {
        public override InsertQuery<T> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions)
        {
            return new InsertQuery<T>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}