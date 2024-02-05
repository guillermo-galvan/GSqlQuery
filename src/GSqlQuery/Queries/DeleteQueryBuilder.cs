using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <param name="formats">Formats</param>
    internal abstract class DeleteQueryBuilder<T, TReturn>(IFormats formats) : QueryBuilderWithCriteria<T, TReturn>(formats)
        where T : class
        where TReturn : DeleteQuery<T>
    {
        protected readonly object _entity;

        public DeleteQueryBuilder(object entity, IFormats formats) : this(formats)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQuery()
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
        internal string CreateQueryByEntty()
        {
            _criteria = GetUpdateCliterias();
            string criteria = GetCriteria();
            return ConstFormat.DELETEWHERE.Replace("{0}", _tableName).Replace("{1}", criteria);
        }

        /// <summary>
        /// Get values
        /// </summary>
        /// <returns>AutoIncrementingClass</returns>
        private Queue<CriteriaDetail> GetUpdateCliterias()
        {
            Queue<CriteriaDetail> criteriaDetails = new Queue<CriteriaDetail>();
            int count = 0;

            foreach (PropertyOptions item in Columns)
            {
                object value = GeneralExtension.GetValue(item, _entity);
                string paramName = "@PD" + Helpers.GetIdParam().ToString();
                string columName = Options.GetColumnName(_tableName, item.ColumnAttribute, QueryType.Criteria);
                string partQuery = (count++ == 0 ? string.Empty : "AND ") + columName + "=" + paramName;
                ParameterDetail parameterDetail = new ParameterDetail(paramName, value, item);
                CriteriaDetail criteriaDetail = new CriteriaDetail(partQuery, [parameterDetail]);
                criteriaDetails.Enqueue(criteriaDetail);
            }
            return criteriaDetails;
        }
    }

    /// <summary>
    /// Delete Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    internal class DeleteQueryBuilder<T> : DeleteQueryBuilder<T, DeleteQuery<T>>
        where T : class
    {
        /// <summary>
        /// Delete Query Builder
        /// </summary>
        /// <param name="formats">Formats</param>
        public DeleteQueryBuilder(IFormats formats) : base(formats)
        { }

        /// <summary>
        /// Delete Query Builder
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="formats">Formats</param>
        public DeleteQueryBuilder(object entity, IFormats formats) : base(entity,formats)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Count Query</returns>
        public override DeleteQuery<T> Build()
        {
            string text = _entity == null ? CreateQuery() : CreateQueryByEntty();
            return new DeleteQuery<T>(text, Columns, _criteria, Options);
        }
    }
}