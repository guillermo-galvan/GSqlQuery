using GSqlQuery.Cache;
using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSqlQuery.Queries
{
    internal class UpdateColumns
    {
        public Expression Expression { get; set; }

        public object Value { get; set; }

        public UpdateColumns(Expression expression)
        {
            Expression = expression;
        }

        public UpdateColumns(Expression expression, object value) :  this(expression)
        {
            Value = value;
        }
    }

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
        private readonly List<UpdateColumns> _columnValues= [] ;
        protected readonly object _entity;

        public List<UpdateColumns> ColumnValues => _columnValues;

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
        public UpdateQueryBuilder(TQueryOptions queryOptions, Expression expression, object value) :
            this(queryOptions)
        {
            _columnValues.Add(new UpdateColumns(expression, value)); 
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryOptions">TQueryOptions</param>
        /// <param name="entity">Entity</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(TQueryOptions queryOptions, object entity, Expression expression) : this(queryOptions)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columnValues.Add(new UpdateColumns(expression, null));
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query Text</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal string CreateQueryText()
        {
            if (_columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }

            List<CriteriaDetailCollection> tmpCriteria = GetUpdateCliterias();
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

                foreach (CriteriaDetailCollection item in _criteria)
                {
                    tmpCriteria.Add(item);
                }

                _criteria = tmpCriteria;
                return ConstFormat.UPDATEWHERE.Replace("{0}", _tableName).Replace("{1}", setParts).Replace("{2}", criterias);
            }
        }

        private List<CriteriaDetailCollection> GetUpdateCliterias()
        {
            List<CriteriaDetailCollection> criteriaDetails = [];
            int count = 0;
            foreach (UpdateColumns item in _columnValues)
            {
                ClassOptionsTupla<PropertyOptionsCollection> properties = ExpressionExtension.GetOptionsAndMembers<T>(item.Expression);
                ExpressionExtension.ValidateClassOptionsTupla(QueryType.Update, properties);

                foreach (KeyValuePair<string, PropertyOptions> column in properties.Columns)
                {
                    string paramName = "@PU" + count++;
                    string columName = column.Value.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Criteria);
                    string partQuery = columName + "=" + paramName;

                    object value = _entity == null ? item.Value : ExpressionExtension.GetValue(column.Value, _entity);
                    ParameterDetail parameterDetail = new ParameterDetail(paramName, value ?? DBNull.Value);
                    CriteriaDetailCollection criteriaDetail = new CriteriaDetailCollection(partQuery, column.Value, [parameterDetail]);
                    criteriaDetails.Add(criteriaDetail);
                }  
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

            _columnValues.Add(new UpdateColumns(expression, value));
        }

        /// <summary>
        /// add to query update another column
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        internal void AddSet<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            _columnValues.Add(new UpdateColumns(expression, null));
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
            AddSet(expression);
            return this;
        }

        public override TReturn Build()
        {
            return CreateQuery();
        }

        public TReturn CreateQuery()
        {
            string text = CreateQueryText();
            return GetQuery(text, Columns, _criteria, QueryOptions);
        }

        public abstract TReturn GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions);
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
        public UpdateQueryBuilder(QueryOptions queryOptions, Expression expression, object value) :
            base(queryOptions, expression, value)
        { }

        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="queryOptions">QueryOptions</param>
        /// <param name="entity">Entity</param>
        /// <param name="selectMember">Name of properties to search</param>
        public UpdateQueryBuilder(QueryOptions queryOptions, object entity, Expression expression) :
           base(queryOptions, entity, expression)
        { }

        public override UpdateQuery<T> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions)
        {
            return new UpdateQuery<T>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}