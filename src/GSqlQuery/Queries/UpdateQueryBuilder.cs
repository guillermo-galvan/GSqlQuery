using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Update query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateQueryBuilder<T> : QueryBuilderWithCriteria<T, UpdateQuery<T>>, IQueryBuilderWithWhere<T, UpdateQuery<T>>,
        ISet<T, UpdateQuery<T>> where T : class, new()
    {
        private readonly IDictionary<ColumnAttribute, object> _columnValues;
        protected readonly object _entity;

        public IDictionary<ColumnAttribute, object> ColumnValues => _columnValues;

        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="options">The Query</param>        
        /// <param name="statements">Statements to build the query</param>
        /// <param name="columnValues">Column values</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(IStatements statements, IEnumerable<string> selectMember, object value) :
            base(statements)
        {
            _columnValues = new Dictionary<ColumnAttribute, object>();
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            foreach (ColumnAttribute item in ClassOptionsFactory.GetClassOptions(typeof(T)).GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        public UpdateQueryBuilder(IStatements statements, object entity, IEnumerable<string> selectMember) :
           base(statements)
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

        internal static string CreateQuery(IDictionary<ColumnAttribute, object> columnValues, bool isWhere, IStatements statements, IEnumerable<PropertyOptions> columns, 
            string tableName, string criterias, ref IEnumerable<CriteriaDetail> criteria)
        {
            if (columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }

            Queue<CriteriaDetail> tmpCriteria = GetUpdateCliterias(columnValues,statements,columns,tableName);
            string query = string.Empty;

            if (!isWhere)
            {
                query = string.Format(statements.Update, tableName, string.Join(",", tmpCriteria.Select(x => x.QueryPart)));
            }
            else
            {
                query = string.Format(statements.UpdateWhere, tableName, string.Join(",", tmpCriteria.Select(x => x.QueryPart)), criterias);
                foreach (var item in criteria)
                {
                    tmpCriteria.Enqueue(item);
                }
            }
            criteria = tmpCriteria;
            return query;
        }

        private static Queue<CriteriaDetail> GetUpdateCliterias(IDictionary<ColumnAttribute, object> columnValues, IStatements statements, IEnumerable<PropertyOptions> columns, string tableName)
        {
            Queue<CriteriaDetail> criteriaDetails = new Queue<CriteriaDetail>();
            long ticks = DateTime.Now.Ticks;
            foreach (var item in columnValues)
            {
                PropertyOptions options = columns.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = $"@PU{ticks++}";
                criteriaDetails.Enqueue(new CriteriaDetail($"{item.Key.GetColumnName(tableName, statements)}={paramName}",
                    new ParameterDetail[] { new ParameterDetail(paramName, item.Value ?? DBNull.Value, options) }));
            }
            return criteriaDetails;
        }

        internal static void AddSet<TProperties>(IDictionary<ColumnAttribute, object> columnValues, 
            System.Linq.Expressions.Expression<Func<T, TProperties>> expression, TProperties value)
        {
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            var column = options.MemberInfo.ValidateMemberInfo(options.ClassOptions).ColumnAttribute;

#if NET5_0_OR_GREATER
            columnValues.TryAdd(column, value);
#else
            if (!columnValues.ContainsKey(column))
            {
                columnValues.Add(column, value);
            }
#endif
        }

        internal static void AddSet<TProperties>(object entity, IDictionary<ColumnAttribute, object> columnValues,
            System.Linq.Expressions.Expression<Func<T, TProperties>> expression)
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
                columnValues.TryAdd(propertyOptions.ColumnAttribute, propertyOptions.GetValue(entity));
#else
                if (!columnValues.ContainsKey(propertyOptions.ColumnAttribute))
                {
                    columnValues.Add(propertyOptions.ColumnAttribute, propertyOptions.GetValue(entity));
                }
#endif
            }
        }

        public override UpdateQuery<T> Build()
        {
            var query = CreateQuery(_columnValues, _andOr != null, Statements, Columns, _tableName, _andOr != null ? GetCriteria() : string.Empty, ref _criteria);
            return new UpdateQuery<T>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        public override IWhere<T, UpdateQuery<T>> Where()
        {
            UpdateWhere<T> selectWhere = new UpdateWhere<T>(this);
            _andOr = selectWhere;
            return (IWhere<T, UpdateQuery<T>>)_andOr;
        }

        public ISet<T, UpdateQuery<T>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression, TProperties value)
        {
            AddSet(_columnValues,expression, value);
            return this;
        }

        public ISet<T, UpdateQuery<T>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression)
        {
            AddSet(_entity, _columnValues, expression);
            return this;
        }
    }
}
