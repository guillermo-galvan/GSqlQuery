using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Update query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateQueryBuilder<T> : QueryBuilderWithCriteria<T, UpdateQuery<T>>, IQueryBuilderWithWhere<T, UpdateQuery<T>>,
        ISet<T, UpdateQuery<T>>  where T : class, new()
    {
        private readonly IDictionary<ColumnAttribute, object?> _columnValues;
        protected readonly object? _entity;

        public IDictionary<ColumnAttribute, object?> ColumnValues => _columnValues;

        /// <summary>
        /// Initializes a new instance of the UpdateQueryBuilder class.
        /// </summary>
        /// <param name="options">The Query</param>        
        /// <param name="statements">Statements to build the query</param>
        /// <param name="columnValues">Column values</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateQueryBuilder(IStatements statements, IEnumerable<string> selectMember, object? value) : 
            base(statements, QueryType.Update)
        {
            _columnValues = new Dictionary<ColumnAttribute, object?>();
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            foreach (ColumnAttribute item in ClassOptionsFactory.GetClassOptions(typeof(T)).GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        public UpdateQueryBuilder(IStatements statements, object? entity, IEnumerable<string> selectMember) :
           base(statements, QueryType.Update)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columnValues = new Dictionary<ColumnAttribute, object?>();
            foreach (var item in from prop in ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions
                                 join sel in selectMember on prop.PropertyInfo.Name equals sel
                                 select new { prop.ColumnAttribute, prop.PropertyInfo })
            {
                _columnValues.Add(item.ColumnAttribute, item.PropertyInfo.GetValue(entity));
            };
        }

        private Queue<CriteriaDetail> GetUpdateCliterias()
        {
            Queue<CriteriaDetail> criteriaDetails = new();

            foreach (var item in _columnValues)
            {
                PropertyOptions options = Columns.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = $"@PU{DateTime.Now.Ticks}";
                criteriaDetails.Enqueue(new CriteriaDetail($"{item.Key.GetColumnName(_tableName, Statements)}={paramName}",
                    new ParameterDetail[] { new ParameterDetail(paramName, item.Value ?? DBNull.Value, options) }));
            }
            return criteriaDetails;
        }

        protected override string GenerateQuery()
        {
            if (_columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }
            Queue<CriteriaDetail> criteria = GetUpdateCliterias();
            string query = string.Empty;
            _criteria = null;

            if (_queryType == QueryType.Update)
            {
                query = string.Format(Statements.Update, _tableName, string.Join(",", criteria.Select(x => x.QueryPart)));
            }
            else
            {
                string where = GetCriteria();
                query = string.Format(Statements.UpdateWhere, _tableName, string.Join(",", criteria.Select(x => x.QueryPart)), where);
                foreach (var item in _criteria!)
                {
                    criteria.Enqueue(item);
                }
            }

            _criteria = criteria;
            return query;
        }

        /// <summary>
        ///  Build update query
        /// </summary>
        /// <returns>UpdateQuery</returns>
        public override UpdateQuery<T> Build()
        {
            return new UpdateQuery<T>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>IWhere</returns>
        public override IWhere<T, UpdateQuery<T>> Where()
        {
            ChangeQueryType();
            UpdateWhere<T> selectWhere = new(this);
            _andOr = selectWhere;
            return (IWhere<T, UpdateQuery<T>>)_andOr;
        }

        public ISet<T, UpdateQuery<T>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression, TProperties value)
        {
            var (options, memberInfos) = expression.GetOptionsAndMember();
            var column = memberInfos.ValidateMemberInfo(options).ColumnAttribute;
            _columnValues.TryAdd(column, value);
            return this;
        }

        public ISet<T, UpdateQuery<T>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression)
        {
            if (_entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }

            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Update(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Update(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");

            foreach (var item in memberInfos)
            {
                var propertyOptions = item.ValidateMemberInfo(options);
                _columnValues.TryAdd(propertyOptions.ColumnAttribute, propertyOptions.GetValue(_entity));
            }

            return this;
        }
    }

    internal class UpdateQueryBuilder<T, TDbConnection> : QueryBuilderWithCriteria<T, UpdateQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhere<T, UpdateQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilder<T, UpdateQuery<T, TDbConnection>, TDbConnection>, IBuilder<UpdateQuery<T, TDbConnection>>,
        ISet<T, UpdateQuery<T, TDbConnection>>
        where T : class, new()
    {
        private readonly IDictionary<ColumnAttribute, object?> _columnValues;
        protected readonly object? _entity;

        public IDictionary<ColumnAttribute, object?> ColumnValues => _columnValues;

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, IEnumerable<string> selectMember, object? value) :
            base(connectionOptions, QueryType.Update)
        {
            _columnValues = new Dictionary<ColumnAttribute,object?>();
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            foreach (ColumnAttribute item in ClassOptionsFactory.GetClassOptions(typeof(T)).GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        public UpdateQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions, object? entity, IEnumerable<string> selectMember) :
            base(connectionOptions, QueryType.Update)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columnValues = new Dictionary<ColumnAttribute, object?>();
            foreach (var item in from prop in ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions
                                 join sel in selectMember on prop.PropertyInfo.Name equals sel
                                 select new { prop.ColumnAttribute, prop.PropertyInfo })
            {
                _columnValues.Add(item.ColumnAttribute, item.PropertyInfo.GetValue(entity));
            };
        }

        private Queue<CriteriaDetail> GetUpdateCliterias()
        {
            Queue<CriteriaDetail> criteriaDetails = new();

            foreach (var item in _columnValues)
            {
                PropertyOptions options = Columns.First(x => x.ColumnAttribute.Name == item.Key.Name);
                string paramName = $"@PU{DateTime.Now.Ticks}";
                criteriaDetails.Enqueue(new CriteriaDetail($"{item.Key.GetColumnName(_tableName, ConnectionOptions.Statements)}={paramName}",
                    new ParameterDetail[] { new ParameterDetail(paramName, item.Value ?? DBNull.Value, options) }));
            }
            return criteriaDetails;
        }

        protected override string GenerateQuery()
        {
            if (_columnValues == null)
            {
                throw new InvalidOperationException("Column values not found");
            }
            Queue<CriteriaDetail> criteria = GetUpdateCliterias();
            string query = string.Empty;
            _criteria = null;

            if (_queryType == QueryType.Update)
            {
                query = string.Format(ConnectionOptions.Statements.Update, _tableName, string.Join(",", criteria.Select(x => x.QueryPart)));
            }
            else
            {
                string where = GetCriteria();
                query = string.Format(ConnectionOptions.Statements.UpdateWhere, _tableName, string.Join(",", criteria.Select(x => x.QueryPart)), where);
                foreach (var item in _criteria!)
                {
                    criteria.Enqueue(item);
                }
            }

            _criteria = criteria;
            return query;
        }

        public override UpdateQuery<T, TDbConnection> Build()
        {
            return new UpdateQuery<T, TDbConnection>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, ConnectionOptions);
        }

        public override IWhere<T, UpdateQuery<T, TDbConnection>> Where()
        {
            ChangeQueryType();
            UpdateWhere<T, TDbConnection> selectWhere = new(this);
            _andOr = selectWhere;
            return (IWhere<T, UpdateQuery<T, TDbConnection>>)_andOr;
        }

        public ISet<T, UpdateQuery<T, TDbConnection>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression, TProperties value)
        {
            var (options, memberInfos) = expression.GetOptionsAndMember();
            var column = memberInfos.ValidateMemberInfo(options).ColumnAttribute;
            _columnValues.TryAdd(column, value);
            return this;
        }

        public ISet<T, UpdateQuery<T, TDbConnection>> Set<TProperties>(System.Linq.Expressions.Expression<Func<T, TProperties>> expression)
        {
            if (_entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }

            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Update(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Update(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");

            foreach (var item in memberInfos)
            {
                var propertyOptions = item.ValidateMemberInfo(options);
                _columnValues.TryAdd(propertyOptions.ColumnAttribute, propertyOptions.GetValue(_entity));
            }

            return this;
        }
    }
}
