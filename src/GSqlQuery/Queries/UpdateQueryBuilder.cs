using GSqlQuery.Extensions;
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
    internal abstract class UpdateQueryBuilder<T, TReturn> : QueryBuilderWithCriteria<T, TReturn>
        where T : class
        where TReturn : UpdateQuery<T>
    {
        private readonly IDictionary<ColumnAttribute, object> _columnValues;
        protected readonly object _entity;

        public IDictionary<ColumnAttribute, object> ColumnValues => _columnValues;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        private UpdateQueryBuilder(IFormats formats) : base(formats)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="value">Value for update</param>
        public UpdateQueryBuilder(IFormats formats, IEnumerable<string> selectMember, object value) :
            this(formats)
        {
            _columnValues = new Dictionary<ColumnAttribute, object>();
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            foreach (ColumnAttribute item in ClassOptionsFactory.GetClassOptions(typeof(T)).GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <param name="entity">Entity</param>
        /// <param name="selectMember">Name of properties to search</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(IFormats formats, object entity, IEnumerable<string> selectMember) :
           this(formats)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columnValues = new Dictionary<ColumnAttribute, object>();
            foreach (var item in from prop in ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions
                                 join sel in selectMember on prop.PropertyInfo.Name equals sel
                                 select new { prop.ColumnAttribute, prop.PropertyInfo })
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

        private Queue<CriteriaDetail> GetUpdateCliterias(IDictionary<ColumnAttribute, object> columnValues,IEnumerable<PropertyOptions> columns, string tableName)
        {
            Queue<CriteriaDetail> criteriaDetails = new Queue<CriteriaDetail>();
            foreach (KeyValuePair<ColumnAttribute, object> item in columnValues)
            {
                PropertyOptions options = columns.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = "@PU" + Helpers.GetIdParam().ToString();
                string columName = Options.GetColumnName(tableName, item.Key, QueryType.Criteria);
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
        /// <returns>Instance of ISet</returns>
        internal void AddSet<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            ColumnAttribute column = options.MemberInfo.ValidateMemberInfo(options.ClassOptions).ColumnAttribute;

            if (!_columnValues.ContainsKey(column))
            {
                _columnValues.Add(column, value);
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
            if (entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Update, options);

            foreach (MemberInfo item in options.MemberInfo)
            {
                PropertyOptions propertyOptions = item.ValidateMemberInfo(options.ClassOptions);

                if (!_columnValues.ContainsKey(propertyOptions.ColumnAttribute))
                {
                    _columnValues.Add(propertyOptions.ColumnAttribute, GeneralExtension.GetValue(propertyOptions,entity));
                }
            }
        }
    }

    /// <summary>
    /// Update query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateQueryBuilder<T> : UpdateQueryBuilder<T, UpdateQuery<T>>,
        ISet<T, UpdateQuery<T>, IFormats> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>        
        /// <param name="formats">Formats</param>
        /// <param name="value">Value for update</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(IFormats formats, IEnumerable<string> selectMember, object value) :
            base(formats, selectMember, value)
        { }

        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <param name="entity">Entity</param>
        /// <param name="selectMember">Name of properties to search</param>
        public UpdateQueryBuilder(IFormats formats, object entity, IEnumerable<string> selectMember) :
           base(formats, entity, selectMember)
        { }

        public override UpdateQuery<T> Build()
        {
            string text = CreateQuery();
            return new UpdateQuery<T>(text, Columns, _criteria, Options);
        }

        /// <summary>
        /// add to query update another column with value
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property</param>
        /// <param name="value"></param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>, IFormats> Set<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
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
        public ISet<T, UpdateQuery<T>, IFormats> Set<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            AddSet(_entity, expression);
            return this;
        }
    }
}