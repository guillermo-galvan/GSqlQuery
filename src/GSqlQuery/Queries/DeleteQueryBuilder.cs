using GSqlQuery.Cache;
using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">QueryOptions</typeparam>
    /// <param name="queryOptions">TQueryOptions</param>
    internal abstract class DeleteQueryBuilder<T, TReturn, TQueryOptions>(TQueryOptions queryOptions) : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>(queryOptions)
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected readonly object _entity;

        public DeleteQueryBuilder(object entity, TQueryOptions queryOptions) : this(queryOptions)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQueryText()
        {
            if (_andOr == null)
            {
                return ConstFormat.DELETE.Replace("{0}", _tableName);
            }
            else
            {
                string criteria = GetCriteria();
                return ConstFormat.DELETEWHERE.Replace("{0}", _tableName).Replace("{1}", criteria);
            }
        }

        /// <summary>
        /// Create query by entity
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQueryTextByEntty()
        {
            _criteria = GetUpdateCliterias();
            string criteria = GetCriteria();
            return ConstFormat.DELETEWHERE.Replace("{0}", _tableName).Replace("{1}", criteria);
        }

        /// <summary>
        /// Get values
        /// </summary>
        /// <returns>AutoIncrementingClass</returns>
        private List<CriteriaDetailCollection> GetUpdateCliterias()
        {
            List<CriteriaDetailCollection> criteriaDetails = [];
            int count = 0;

            foreach (PropertyOptions item in Columns.Values)
            {
                object value = ExpressionExtension.GetValue(item, _entity);
                string paramName = "@PD" + count;
                string columName = item.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Criteria);
                string partQuery = (count++ == 0 ? string.Empty : "AND ") + columName + "=" + paramName;
                ParameterDetail parameterDetail = new ParameterDetail(paramName, value);
                CriteriaDetailCollection criteriaDetail = new CriteriaDetailCollection(partQuery, item, [parameterDetail]);
                criteriaDetails.Add(criteriaDetail);
            }
            return criteriaDetails;
        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Count Query</returns>
        public override TReturn Build()
        {
            return CacheQueryBuilderExtension.CreateDeleteQuery<T, TReturn, TQueryOptions>(QueryOptions, _andOr, _entity, CreateQuery, GetQuery);
        }

        public TReturn CreateQuery()
        {
            string text = _entity == null ? CreateQueryText() : CreateQueryTextByEntty();
            return GetQuery(text, Columns, _criteria, QueryOptions);
        }

        public abstract TReturn GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions);
    }

    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    internal class DeleteQueryBuilder<T> : DeleteQueryBuilder<T, DeleteQuery<T>, QueryOptions>
        where T : class
    {
        /// <summary>
        /// Delete Query Builder
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        public DeleteQueryBuilder(QueryOptions queryOptions) : base(queryOptions)
        { }

        /// <summary>
        /// Delete Query Builder
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="queryOptions">QueryOptions</param>
        public DeleteQueryBuilder(object entity, QueryOptions queryOptions) : base(entity, queryOptions)
        { }

        public override DeleteQuery<T> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions)
        {
            return new DeleteQuery<T>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}