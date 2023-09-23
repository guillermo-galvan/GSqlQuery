using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Queries
{
    internal abstract class UpdateQueryBuilder<T, TReturn> : QueryBuilderWithCriteria<T, TReturn>
        where T : class
        where TReturn : UpdateQuery<T>
    {
        private readonly IDictionary<ColumnAttribute, object> _columnValues;
        protected readonly object _entity;

        public IDictionary<ColumnAttribute, object> ColumnValues => _columnValues;

        private UpdateQueryBuilder(IFormats statements) : base(statements)
        { }

        public UpdateQueryBuilder(IFormats statements, IEnumerable<string> selectMember, object value) :
            this(statements)
        {
            _columnValues = new Dictionary<ColumnAttribute, object>();
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            foreach (ColumnAttribute item in ClassOptionsFactory.GetClassOptions(typeof(T)).GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        public UpdateQueryBuilder(IFormats statements, object entity, IEnumerable<string> selectMember) :
           this(statements)
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

        internal string CreateQuery(IFormats statements)
        {
            if (_columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }

            Queue<CriteriaDetail> tmpCriteria = GetUpdateCliterias(_columnValues, statements, Columns, _tableName);
            string query = string.Empty;

            if (_andOr == null)
            {
                query = string.Format(ConstFormat.UPDATE, _tableName, string.Join(",", tmpCriteria.Select(x => x.QueryPart)));
            }
            else
            {
                query = string.Format(ConstFormat.UPDATEWHERE, _tableName, string.Join(",", tmpCriteria.Select(x => x.QueryPart)), GetCriteria());
                foreach (var item in _criteria)
                {
                    tmpCriteria.Enqueue(item);
                }
            }
            _criteria = tmpCriteria;
            return query;
        }

        private Queue<CriteriaDetail> GetUpdateCliterias(IDictionary<ColumnAttribute, object> columnValues, IFormats statements, IEnumerable<PropertyOptions> columns, string tableName)
        {
            Queue<CriteriaDetail> criteriaDetails = new Queue<CriteriaDetail>();
            foreach (var item in columnValues)
            {
                PropertyOptions options = columns.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = $"@PU{Helpers.GetIdParam()}";
                criteriaDetails.Enqueue(new CriteriaDetail($"{item.Key.GetColumnName(tableName, statements, QueryType.Criteria)}={paramName}",
                    new ParameterDetail[] { new ParameterDetail(paramName, item.Value ?? DBNull.Value, options) }));
            }
            return criteriaDetails;
        }

        internal void AddSet<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            var column = options.MemberInfo.ValidateMemberInfo(options.ClassOptions).ColumnAttribute;

#if NET5_0_OR_GREATER
            _columnValues.TryAdd(column, value);
#else
            if (!_columnValues.ContainsKey(column))
            {
                _columnValues.Add(column, value);
            }
#endif
        }

        internal void AddSet<TProperties>(object entity, Expression<Func<T, TProperties>> expression)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.ClassOptions.Type.Name}.Update(x => x.{options.ClassOptions.PropertyOptions.First().PropertyInfo.Name}) or {options.ClassOptions.Type.Name}.Update(x => new {{ {string.Join(",", options.ClassOptions.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");

            foreach (var item in options.MemberInfo)
            {
                var propertyOptions = item.ValidateMemberInfo(options.ClassOptions);

#if NET5_0_OR_GREATER
                _columnValues.TryAdd(propertyOptions.ColumnAttribute, propertyOptions.GetValue(entity));
#else
                if (!_columnValues.ContainsKey(propertyOptions.ColumnAttribute))
                {
                    _columnValues.Add(propertyOptions.ColumnAttribute, propertyOptions.GetValue(entity));
                }
#endif
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
        /// <param name="options">The Query</param>        
        /// <param name="statements">Statements to build the query</param>
        /// <param name="columnValues">Column values</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(IFormats statements, IEnumerable<string> selectMember, object value) :
            base(statements, selectMember, value)
        { }

        public UpdateQueryBuilder(IFormats statements, object entity, IEnumerable<string> selectMember) :
           base(statements, entity, selectMember)
        { }

        public override UpdateQuery<T> Build()
        {
            return new UpdateQuery<T>(CreateQuery(Options), Columns, _criteria, Options);
        }

        public ISet<T, UpdateQuery<T>, IFormats> Set<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            AddSet(expression, value);
            return this;
        }

        public ISet<T, UpdateQuery<T>, IFormats> Set<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            AddSet(_entity, expression);
            return this;
        }
    }
}