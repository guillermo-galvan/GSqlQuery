﻿using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    ///  Update Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    internal abstract class UpdateQueryBuilder<T, TReturn, TQueryOptions> : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>, ISet<T, TReturn, TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
         where TQueryOptions : QueryOptions
    {
        private readonly IDictionary<ColumnAttribute, object> _columnValues;
        protected readonly object _entity;

        public IDictionary<ColumnAttribute, object> ColumnValues => _columnValues;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryOptions">Formats</param>
        private UpdateQueryBuilder(TQueryOptions queryOptions) : base(queryOptions)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryOptions">TQueryOptions</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="value">Value for update</param>
        public UpdateQueryBuilder(TQueryOptions queryOptions, ClassOptionsTupla<MemberInfo> classOptionsTupla, object value) :
            this(queryOptions)
        {
            _columnValues = new Dictionary<ColumnAttribute, object>();
            ColumnAttribute columnAttributes = ExpressionExtension.GetColumnQuery(classOptionsTupla ?? throw new ArgumentNullException(nameof(classOptionsTupla)));
            _columnValues.Add(columnAttributes, value);
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryOptions">TQueryOptions</param>
        /// <param name="entity">Entity</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(TQueryOptions queryOptions, object entity, ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla) : this(queryOptions)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columnValues = new Dictionary<ColumnAttribute, object>();
            IEnumerable<PropertyOptions> properties = ExpressionExtension.GetPropertyQuery(classOptionsTupla);

            foreach (PropertyOptions item in properties)
            {
                _columnValues.Add(item.ColumnAttribute, item.PropertyInfo.GetValue(entity));
            };
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query Text</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal string CreateQuery()
        {
            if (_columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }

            Queue<CriteriaDetail> tmpCriteria = GetUpdateCliterias(_columnValues, Columns, _tableName);
            IEnumerable<string> queryParts = tmpCriteria.Select(x => x.QueryPart);
            string setParts = string.Join(",", queryParts);
            if (_andOr == null)
            {
                _criteria = tmpCriteria;
                return ConstFormat.UPDATE.Replace("{0}", _tableName).Replace("{1}", setParts);
            }
            else
            {
                string criterias = GetCriteria();

                foreach (CriteriaDetail item in _criteria)
                {
                    tmpCriteria.Enqueue(item);
                }

                _criteria = tmpCriteria;
                return ConstFormat.UPDATEWHERE.Replace("{0}", _tableName).Replace("{1}", setParts).Replace("{2}", criterias);
            }
        }

        private Queue<CriteriaDetail> GetUpdateCliterias(IDictionary<ColumnAttribute, object> columnValues, IEnumerable<PropertyOptions> columns, string tableName)
        {
            Queue<CriteriaDetail> criteriaDetails = new Queue<CriteriaDetail>();
            foreach (KeyValuePair<ColumnAttribute, object> item in columnValues)
            {
                PropertyOptions options = columns.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = "@PU" + Helpers.GetIdParam().ToString();
                string columName = QueryOptions.Formats.GetColumnName(tableName, item.Key, QueryType.Criteria);
                string partQuery = columName + "=" + paramName;
                ParameterDetail parameterDetail = new ParameterDetail(paramName, item.Value ?? DBNull.Value, options);
                CriteriaDetail criteriaDetail = new CriteriaDetail(partQuery, [parameterDetail]);
                criteriaDetails.Enqueue(criteriaDetail);
            }
            return criteriaDetails;
        }

        /// <summary>
        /// add to query update another column with value
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property</param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Instance of ISet</returns>
        internal void AddSet<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<MemberInfo> options = ExpressionExtension.GetOptionsAndMember(expression);
            PropertyOptions propertyOptions = ExpressionExtension.ValidateMemberInfo(options.MemberInfo, options.ClassOptions);

            if (!_columnValues.ContainsKey(propertyOptions.ColumnAttribute))
            {
                _columnValues.Add(propertyOptions.ColumnAttribute, value);
            }
        }

        /// <summary>
        /// add to query update another column
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        internal void AddSet<TProperties>(object entity, Expression<Func<T, TProperties>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            if (entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression, _classOptions);
            ExpressionExtension.ValidateMemberInfos(QueryType.Update, options);

            foreach (MemberInfo item in options.MemberInfo)
            {
                PropertyOptions propertyOptions = ExpressionExtension.ValidateMemberInfo(item, options.ClassOptions);

                if (!_columnValues.ContainsKey(propertyOptions.ColumnAttribute))
                {
                    object value = ExpressionExtension.GetValue(propertyOptions, entity);
                    _columnValues.Add(propertyOptions.ColumnAttribute, value);
                }
            }
        }

        /// <summary>
        /// add to query update another column with value
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property</param>
        /// <param name="value"></param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, TReturn, TQueryOptions> Set<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            AddSet(expression, value);
            return this;
        }

        /// <summary>
        /// add to query update another column
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, TReturn, TQueryOptions> Set<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            AddSet(_entity, expression);
            return this;
        }
    }

    /// <summary>
    /// Update query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateQueryBuilder<T> : UpdateQueryBuilder<T, UpdateQuery<T>, QueryOptions>,
        ISet<T, UpdateQuery<T>, QueryOptions> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>        
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="value">Value for update</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(QueryOptions queryOptions, ClassOptionsTupla<MemberInfo> classOptionsTupla, object value) :
            base(queryOptions, classOptionsTupla, value)
        { }

        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="entity">Entity</param>
        /// <param name="selectMember">Name of properties to search</param>
        public UpdateQueryBuilder(QueryOptions queryOptions, object entity, ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla) :
           base(queryOptions, entity, classOptionsTupla)
        { }

        public override UpdateQuery<T> Build()
        {
            string text = CreateQuery();
            return new UpdateQuery<T>(text, Columns, _criteria, QueryOptions);
        }
    }
}